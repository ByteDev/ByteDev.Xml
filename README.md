[![Build status](https://ci.appveyor.com/api/projects/status/github/bytedev/ByteDev.Xml?branch=master&svg=true)](https://ci.appveyor.com/project/bytedev/ByteDev-Xml/branch/master)
[![NuGet Package](https://img.shields.io/nuget/v/ByteDev.Xml.svg)](https://www.nuget.org/packages/ByteDev.Xml)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://github.com/ByteDev/ByteDev.Xml/blob/master/LICENSE)

# ByteDev.Xml

.NET Standard library of XML related functionality.

## Installation

ByteDev.Xml has been written as a .NET Standard 2.0 library, so you can consume it from a .NET Core or .NET Framework 4.6.1 (or greater) application.

ByteDev.Xml is hosted as a package on nuget.org.  To install from the Package Manager Console in Visual Studio run:

`Install-Package ByteDev.Xml`

Further details can be found on the [nuget page](https://www.nuget.org/packages/ByteDev.Xml/).

## Release Notes

Releases follow semantic versioning.

Full details of the release notes can be viewed on [GitHub](https://github.com/ByteDev/ByteDev.Xml/blob/master/docs/RELEASE-NOTES.md).

## Usage

Extension methods:

XDocument
- IsRootName

XElement
- GetChildElement
- GetChildElements
- GetChildElementValue
- GetChildElementValue<TValue>
- GetAttributeValue
- GetAttributeValue<TValue>
- HasDescendants

IEnumerable<XElement>
- GetChildElement
- GetChildElements

String
- IsXml
- ContainsOnlyXmlChars

---

### XmlDataSerializer

Example of serializing and deserializing:

```csharp
[XmlRoot("product")]
public class ProductXml
{
    [XmlElement("code")]
    public string Code { get; set; }

    [XmlElement("name")]
    public string Name { get; set; }
}

// ...

var product = new ProductXml { Code = "code1", Name = "name1" };

IXmlDataSerializer serializer = new XmlDataSerializer();

var xml = serializer.Serialize(product, Encoding.UTF8);

var p = serializer.Deserialize<ProductXml>(xml);
```

---

### XmlEncoder

Example of encoding a XML string:

```csharp
var result = XmlEncoder.Encode("Using & special 'entity'");

// result == "Using &amp; special &apos;entity&apos;"
```

---

### XmlSanitizer

Example of removing illegal XML characters from a XML string:

```csharp
char illegalChar = '\0';

string s = $"this {illegalChar} that {illegalChar} this";

var result = XmlSanitizer.Sanitize(s);

// result == "this  that  this"
```