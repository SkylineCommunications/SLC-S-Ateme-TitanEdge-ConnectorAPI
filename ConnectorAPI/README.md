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

### Configure an Output Channel

```csharp
using Skyline.DataMiner.Utils.AtemeTitanEdge;

var config = new OutputConfiguration(0, 1, ConfigType.Decoder)
    .ChangeType(SdiType.HdSdi)
    .SetName("Main Output")
    .EnableColorimetry(true)
    .SetColorimetryConversion(ColorimetryConversion.Hdr10)
    .SetIutName(IutName.Hable);

config.Send(client); // client implements IAtemeTitanEdgeClient
```

### Configure an Input Channel

```csharp
using Skyline.DataMiner.Utils.AtemeTitanEdge;

var config = new InputConfiguration(0, 1, ConfigType.Encoder)
    .EnableInput(true)
    .SetIpAddress("239.0.0.1")
    .SetIpPort(5000)
    .SetInputType(InputType.Ip)
    .EnableInputFec(true);

config.Send(client); // client implements IAtemeTitanEdgeClient
```

### Error Handling Example

```csharp
try
{
    var config = new InputConfiguration(0, 1, ConfigType.Encoder)
        .SetIpAddress("invalid_ip");
}
catch (ArgumentException ex)
{
    // Handle invalid IP address
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
