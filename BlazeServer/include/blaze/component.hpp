#pragma once

#include "blaze/types.hpp"
#include "blaze/packet.hpp"
#include <memory>
#include <string>

namespace ds2::network {
    class ClientConnection;
}

namespace ds2::blaze {

/**
 * Base class for Blaze components
 * 
 * Each component handles a specific category of messages
 * (Authentication, GameManager, Redirector, etc.)
 */
class Component {
public:
    Component(ComponentId id, const std::string& name);
    virtual ~Component() = default;
    
    // Get component ID
    ComponentId getId() const { return m_id; }
    
    // Get component name
    const std::string& getName() const { return m_name; }
    
    // Handle incoming packet - returns response packet
    virtual std::unique_ptr<Packet> handlePacket(
        const Packet& request,
        std::shared_ptr<network::ClientConnection> client
    ) = 0;
    
protected:
    ComponentId m_id;
    std::string m_name;
    
    // Helper to create standard error responses
    std::unique_ptr<Packet> createError(const Packet& request, BlazeError error);
    
    // Helper to create success response
    std::unique_ptr<Packet> createSuccess(const Packet& request, const TdfStruct& data);
};

/**
 * Component registry - manages all components
 */
class ComponentRegistry {
public:
    static ComponentRegistry& instance();
    
    // Register a component
    void registerComponent(std::shared_ptr<Component> component);
    
    // Get component by ID
    std::shared_ptr<Component> getComponent(ComponentId id);
    
    // Route packet to appropriate component
    std::unique_ptr<Packet> routePacket(
        const Packet& request,
        std::shared_ptr<network::ClientConnection> client
    );
    
private:
    ComponentRegistry() = default;
    std::map<ComponentId, std::shared_ptr<Component>> m_components;
};

} // namespace ds2::blaze
