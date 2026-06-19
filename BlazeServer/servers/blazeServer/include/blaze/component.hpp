#pragma once

#include "blaze/types.hpp"
#include "blaze/packet.hpp"
#include <memory>
#include <string>

namespace gw2::network {
    class ClientConnection;
}

namespace gw2::blaze {

class Component {
public:
    Component(ComponentId id, const std::string& name);
    virtual ~Component() = default;
    
    ComponentId getId() const { return m_id; }
    
    const std::string& getName() const { return m_name; }
    
    virtual std::unique_ptr<Packet> handlePacket(
        const Packet& request,
        std::shared_ptr<network::ClientConnection> client
    ) = 0;
    
protected:
    ComponentId m_id;
    std::string m_name;
    
    std::unique_ptr<Packet> createError(const Packet& request, BlazeError error);
    
    std::unique_ptr<Packet> createSuccess(const Packet& request, const TdfStruct& data);
};

class ComponentRegistry {
public:
    static ComponentRegistry& instance();
    
    void registerComponent(std::shared_ptr<Component> component);
    
    std::shared_ptr<Component> getComponent(ComponentId id);
    
    std::unique_ptr<Packet> routePacket(
        const Packet& request,
        std::shared_ptr<network::ClientConnection> client
    );
    
private:
    ComponentRegistry() = default;
    std::map<ComponentId, std::shared_ptr<Component>> m_components;
};

} // namespace gw2::blaze
