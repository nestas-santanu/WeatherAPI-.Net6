using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WeatherAPI.Filters.Swagger
{
    public class RemoveSchemasFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Components.Schemas.Remove("Assembly");
            swaggerDoc.Components.Schemas.Remove("CallingConventions");
            swaggerDoc.Components.Schemas.Remove("ConstructorInfo");
            swaggerDoc.Components.Schemas.Remove("CustomAttributeData");
            swaggerDoc.Components.Schemas.Remove("CustomAttributeNamedArgument");
            swaggerDoc.Components.Schemas.Remove("CustomAttributeTypedArgument");
            swaggerDoc.Components.Schemas.Remove("EventAttributes");
            swaggerDoc.Components.Schemas.Remove("EventInfo");
            swaggerDoc.Components.Schemas.Remove("Exception");
            swaggerDoc.Components.Schemas.Remove("FieldAttributes");
            swaggerDoc.Components.Schemas.Remove("FieldInfo");
            swaggerDoc.Components.Schemas.Remove("GenericParameterAttributes");
            swaggerDoc.Components.Schemas.Remove("ICustomAttributeProvider");
            swaggerDoc.Components.Schemas.Remove("IntPtr");
            swaggerDoc.Components.Schemas.Remove("LayoutKind");
            swaggerDoc.Components.Schemas.Remove("MemberInfo");
            swaggerDoc.Components.Schemas.Remove("MemberTypes");
            swaggerDoc.Components.Schemas.Remove("MethodAttributes");
            swaggerDoc.Components.Schemas.Remove("MethodBase");
            swaggerDoc.Components.Schemas.Remove("MethodImplAttributes");
            swaggerDoc.Components.Schemas.Remove("MethodInfo");
            swaggerDoc.Components.Schemas.Remove("Module");
            swaggerDoc.Components.Schemas.Remove("ModuleHandle");
            swaggerDoc.Components.Schemas.Remove("ParameterAttributes");
            swaggerDoc.Components.Schemas.Remove("ParameterInfo");
            swaggerDoc.Components.Schemas.Remove("PropertyAttributes");
            swaggerDoc.Components.Schemas.Remove("PropertyInfo");
            swaggerDoc.Components.Schemas.Remove("RuntimeFieldHandle");
            swaggerDoc.Components.Schemas.Remove("RuntimeMethodHandle");
            swaggerDoc.Components.Schemas.Remove("RuntimeTypeHandle");
            swaggerDoc.Components.Schemas.Remove("SecurityRuleSet");
            swaggerDoc.Components.Schemas.Remove("StructLayoutAttribute");
            swaggerDoc.Components.Schemas.Remove("Type");
            swaggerDoc.Components.Schemas.Remove("TypeAttributes");
            swaggerDoc.Components.Schemas.Remove("TypeInfo");

            ////all schemas are found here
            //IDictionary<string, OpenApiSchema> schemas = swaggerDoc.Components.Schemas;

            ////foreach (KeyValuePair<string, OpenApiSchema> item in schemas)
            ////{
            ////    swaggerDoc.Components.Schemas.Remove("Assembly");
            ////}
        }
    }
}