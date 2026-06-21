#include "blaze/component.hpp"
#include "network/client_connection.hpp"
#include "utils/logger.hpp"

namespace gw2::blaze {

Component::Component(ComponentId id, const std::string& name)
    : m_id(id), m_name(name) {}

std::unique_ptr<Packet> Component::createError(const Packet& request, BlazeError error) {
    return request.createErrorReply(error);
}

std::unique_ptr<Packet> Component::createSuccess(const Packet& request, const TdfStruct& data) {
    auto reply = request.createReply();
    reply->setPayload(data);
    return reply;
}


ComponentRegistry& ComponentRegistry::instance() {
    static ComponentRegistry registry;
    return registry;
}

void ComponentRegistry::registerComponent(std::shared_ptr<Component> component) {
    m_components[component->getId()] = component;
}

std::shared_ptr<Component> ComponentRegistry::getComponent(ComponentId id) {
    auto it = m_components.find(id);
    if (it != m_components.end()) {
        return it->second;
    }
    return nullptr;
}

std::unique_ptr<Packet> ComponentRegistry::routePacket(
    const Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    ComponentId componentId = request.getComponent();
    uint16_t command = request.getCommand();
    
    auto component = getComponent(componentId);
    if (!component) {
        LOG_WARN("Unknown component: 0x{:04X}, command: 0x{:04X}", 
                 static_cast<uint16_t>(componentId), command);
        return request.createReply();
    }
    
    LOG_DEBUG("[{}] Handling command 0x{:04X}", component->getName(), command);
    return component->handlePacket(request, client);
}

} // namespace gw2::blaze
