﻿using System.Text.Json.Serialization;
using Smusdi.RegexTools;

namespace Smusdi.Wasm;

[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(RegexValidationResult))]
[JsonSerializable(typeof(StringPartsExtractor))]
[JsonSerializable(typeof(StringPartsExtractorValidationResult))]
public partial class SmusdiSerializationContext : JsonSerializerContext
{
}
