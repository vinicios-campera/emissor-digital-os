using NSwag;
using NSwag.CodeGeneration.CSharp;
using System.Runtime.CompilerServices;

namespace OrderService.Api
{
    public static class Generator
    {
        public static void Generate()
        {
            var document = OpenApiDocument.FromUrlAsync("http://192.168.0.101:5000/swagger/v1/swagger.json").Result;
            var settings = new CSharpClientGeneratorSettings
            {
                ClientBaseClass = nameof(ApiClientBase),
                ConfigurationClass = null,
                GenerateClientClasses = true,
                GenerateClientInterfaces = true,
                ClientBaseInterface = nameof(IApiClientBase),
                InjectHttpClient = true,
                DisposeHttpClient = false,
                GenerateExceptionClasses = true,
                WrapDtoExceptions = false,
                UseHttpClientCreationMethod = false,
                HttpClientType = "System.Net.Http.HttpClient",
                UseHttpRequestMessageCreationMethod = true,
                UseBaseUrl = true,
                GenerateBaseUrlProperty = false,
                GenerateSyncMethods = false,
                GeneratePrepareRequestAndProcessResponseAsAsyncMethods = false,
                ExposeJsonSerializerSettings = false,
                ClientClassAccessModifier = "public",
                ParameterDateTimeFormat = "s",
                ParameterDateFormat = "yyyy-MM-dd",
                GenerateUpdateJsonSerializerSettingsMethod = true,
                UseRequestAndResponseSerializationSettings = false,
                SerializeTypeInformation = false,
                QueryNullValue = "",
                ClassName = "OrderServiceApiClient",
                GenerateOptionalParameters = true,
                ParameterArrayType = "System.Collections.Generic.IEnumerable",
                ParameterDictionaryType = "System.Collections.Generic.IDictionary",
                ResponseArrayType = "System.Collections.Generic.IEnumerable",
                ResponseDictionaryType = "System.Collections.Generic.IDictionary",
                WrapResponses = false,
                WrapResponseMethods = Array.Empty<string>(),
                GenerateResponseClasses = true,
                ResponseClass = "SwaggerResponse",
                ExcludedParameterNames = Array.Empty<string>(),
                GenerateDtoTypes = true,
                ProtectedMethods = Array.Empty<string>(),
                CSharpGeneratorSettings =
                {
                    Namespace = "OrderService.Api.Client",
                    TypeAccessModifier = "public",
                    GenerateJsonMethods = false,
                    EnforceFlagEnums = false,
                    RequiredPropertiesMustBeDefined = true,
                    DateType = "System.DateTime",
                    JsonConverters = null,
                    AnyType ="object",
                    DateTimeType = "System.DateTime",
                    TimeType = "System.TimeSpan",
                    TimeSpanType = "System.TimeSpan",
                    ArrayType = "System.Collections.Generic.IEnumerable",
                    ArrayInstanceType = "System.Collections.ObjectModel.Collection",
                    DictionaryType = "System.Collections.Generic.IDictionary",
                    DictionaryInstanceType = "System.Collections.Generic.Dictionary",
                    ArrayBaseType = "System.Collections.ObjectModel.Collection",
                    DictionaryBaseType = "System.Collections.Generic.Dictionary",
                    ClassStyle = NJsonSchema.CodeGeneration.CSharp.CSharpClassStyle.Poco,
                    JsonLibrary = NJsonSchema.CodeGeneration.CSharp.CSharpJsonLibrary.NewtonsoftJson,
                    GenerateDefaultValues = true,
                    GenerateDataAnnotations = true,
                    ExcludedTypeNames = Array.Empty<string>(),
                    HandleReferences = false,
                    GenerateImmutableArrayProperties = false,
                    GenerateImmutableDictionaryProperties = false,
                    JsonSerializerSettingsTransformationMethod = null,
                    InlineNamedArrays = false,
                    InlineNamedDictionaries = false,
                    InlineNamedTuples   = true,
                    InlineNamedAny= false,
                    GenerateOptionalPropertiesAsNullable = false,
                    GenerateNullableReferenceTypes = false,
                    TemplateDirectory = null
                }
            };
            var generator = new CSharpClientGenerator(document, settings);
            var code = generator.GenerateFile();
            var pathName = new { }.GetFilePath();
            var path = $"{Path.Combine(pathName, @"..\")}OrderServiceApiClient.cs";
            Console.WriteLine($"Gerando clientem : {path}");
            File.WriteAllText(path, code);
            Console.WriteLine($"Client gerado com sucesso");
            Console.WriteLine("Press any key to close app...");
            Console.ReadKey();
        }

        public static string GetFilePath(this object obj, [CallerFilePath] string? callerFilePath = null)
        {
            if (obj is null)
                throw new ArgumentNullException(nameof(obj));

            return callerFilePath ?? "";
        }
    }
}