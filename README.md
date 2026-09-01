# Skyline.DataMiner.ConnectorAPI.Ateme.TitanEdge

## About

This repository contains the messages and builder classes used to interact with the Ateme Titan Edge connector using InterApp communication in DataMiner.

## Use Cases

- **Automated Channel Configuration:**
  - Programmatically configure input and output channels for Ateme Titan Edge devices.
- **Bulk Configuration Updates:**
  - Build and send multiple configuration changes in a single operation using the builder pattern.
- **Validation and Error Handling:**
  - Input validation is built-in for IP addresses, ports, and other parameters, ensuring robust configuration.

## Examples

### Send a Single Message

```csharp
using Skyline.DataMiner.ConnectorAPI.Ateme.TitanEdge;
using Skyline.DataMiner.ConnectorAPI.Ateme.TitanEdge.Messages;

var client = new AtemeTitanEdgeClient(connection, "My Ateme Element");

client.SendMessage(new ConfigureEncoderMessage { Pid = 2001, Value = "SRT", PrimaryKey = "1" });
```

### Send Multiple Messages in Bulk

```csharp
using Skyline.DataMiner.ConnectorAPI.Ateme.TitanEdge;
using Skyline.DataMiner.ConnectorAPI.Ateme.TitanEdge.Messages;

var client = new AtemeTitanEdgeClient(connection, "My Ateme Element");

client.SendBulk(new Message[]
{
    new ConfigureDecoderMessage     { Pid = 1001, Value = "1",     PrimaryKey = "1" },
    new ConfigureEncoderMessage     { Pid = 2001, Value = "SRT",   PrimaryKey = "1" },
    new ConfigureDemodulatorMessage { Pid = 3001, Value = "27500", PrimaryKey = "1" },
});
```

### Connect by Agent and Element ID

```csharp
var client = new AtemeTitanEdgeClient(connection, agentId: 123, elementId: 456);
```

### Error Handling Example

```csharp
try
{
    var client = new AtemeTitanEdgeClient(connection, "NonExistentElement");
}
catch (ArgumentException ex)
{
    // Element does not exist or is not running the Ateme Titan Edge protocol
}
```

---

### About DataMiner

DataMiner is a transformational platform that provides vendor-independent control and monitoring of devices and services. Out of the box and by design, it addresses key challenges such as security, complexity, multi-cloud, and much more. It has a pronounced open architecture and powerful capabilities enabling users to evolve easily and continuously.

The foundation of DataMiner is its powerful and versatile data acquisition and control layer. With DataMiner, there are no restrictions to what data users can access. Data sources may reside on premises, in the cloud, or in a hybrid setup.

A unique catalog of 7000+ connectors already exists. In addition, you can leverage DataMiner Development Packages to build your own connectors (also known as "protocols" or "drivers").

> **Note**
> See also: [About DataMiner](https://aka.dataminer.services/about-dataminer).

### About Skyline Communications

At Skyline Communications, we deal in world-class solutions that are deployed by leading companies around the globe. Check out [our proven track record](https://aka.dataminer.services/about-skyline) and see how we make our customers' lives easier by empowering them to take their operations to the next level.
