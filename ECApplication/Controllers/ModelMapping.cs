using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ECApplication.Controllers
{
    public class ModelMapping<TSource, TTarget> where TSource:class where TTarget:class
    {
        public TTarget Mapping(TSource source, TTarget target)
        {
            //var sourceType = source.GetType();
            //var targetType = typeof(TDestinate);
            //var sourceProperties = sourceType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            //var targetProperties = targetType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            //var targetInstance = Activator.CreateInstance<TDestinate>();
            //var mappingAttributeType = typeof(ObjectMappingAttribute);

            //foreach (var sourceProperty in sourceProperties)
            //{
            //    var sourcePropertyName = sourceProperty.Name;
            //    var sourceValue = sourceProperty.GetValue(source);

            //    foreach (var targetProperty in targetProperties)
            //    {
            //        var targetPropertyName = targetProperty.Name;
            //        if (sourcePropertyName == targetPropertyName)
            //        {
            //            if (sourceProperty.PropertyType == targetProperty.PropertyType)
            //            {
            //                targetProperty.SetValue(targetInstance, sourceValue);
            //                break;
            //            }
            //        }
            //        var mappingAttributes = targetProperty.GetCustomAttributes(mappingAttributeType, false);
            //        if (mappingAttributes.Any())
            //        {
            //            var mappingAttributePropertyName = ((ObjectMappingAttribute)mappingAttributes[0]).MappingName;
            //            if (mappingAttributePropertyName == sourcePropertyName)
            //            {
            //                if (sourceProperty.PropertyType == targetProperty.PropertyType)
            //                {
            //                    targetProperty.SetValue(targetInstance, sourceValue);
            //                    break;
            //                }
            //            }

            //        }

            //    }
            //}
            //return targetInstance;

            var SourceProperties = source.GetType().GetProperties();
            var TargetProperties = target.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var MappingAttributeType = typeof(ObjectMappingAttribute);

            foreach (var item in SourceProperties)
            {
                var TargetProp = TargetProperties.Where(p => p.Name == item.Name && p.PropertyType == item.PropertyType).FirstOrDefault();
                if (TargetProp != null)
                {
                    TargetProp.SetValue(target, item.GetValue(source));
                }

                var MappingAttributes = TargetProperties.Where(p => p.GetCustomAttribute(MappingAttributeType, false) != null).Any();

                if (MappingAttributes)
                {
                    var MapProp = TargetProperties.Where(p => (p.GetCustomAttribute(MappingAttributeType, false) as ObjectMappingAttribute).MappingName == item.Name && p.PropertyType == item.PropertyType).FirstOrDefault();
                    if (MapProp != null)
                    {
                        MapProp.SetValue(target, item.GetValue(source));
                    }
                }
            }

            return target;
        }
    }
}
public class ObjectMappingAttribute : Attribute
{
    public string MappingName { get; set; }

    public ObjectMappingAttribute(string mappingName)
    {
        this.MappingName = mappingName;
    }
}