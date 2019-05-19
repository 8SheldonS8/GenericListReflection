using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;

namespace Common
{
    public class DataMapper
    {
        public Dictionary<string, object> GetFieldValue (object person, List<string> requestedFields)
        {
            Dictionary<string, object> returnValue = new Dictionary<string, object>();
            foreach (string field in requestedFields)
            {
                PropertyInfo propInfo = person.GetType().GetProperty(field);
                if (propInfo != null)
                {
                    if (IsGenericList(propInfo))
                    {
                        returnValue.Add(field, ReflectGenericList(propInfo, person));
                    }
                    else
                    {
                        returnValue.Add(field, propInfo.GetValue(person));
                    }
                }
                else
                {
                    returnValue.Add(field, "No available data.");
                }
            }
            
            return returnValue;
        }
        public bool IsGenericList(PropertyInfo propInfo)
        {
            if (propInfo != null)
            {
                Type type = propInfo.PropertyType;
                return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>);
            }
            return false;
        }

        public bool IsICollection(PropertyInfo propInfo)
        {
            bool result = false;
            if (propInfo != null)
            {
                Type type = propInfo.PropertyType;
                result = (type.Namespace == "System.Collections.Generic");
            }
            return result;
        }
        public IDictionary<string, object> ReflectGenericList(PropertyInfo propInfo, object parentObject)
        {
            var valueObject = new ExpandoObject() as IDictionary<string, object>;
            IList itemList = propInfo.GetValue(parentObject, null) as IList;
            int purchaseCount = 1;
            if (itemList != null)
            {
                foreach (var listItem in itemList)
                {
                    object value = new object();
                    string caption = String.Empty;

                    switch (listItem.GetType().Name)
                    {
                        case "Purchase":
                            caption = String.Format("Purchase {0}", purchaseCount);
                            value = String.Format("{0} - {1}", 
                                listItem.GetType().GetProperty("ProductName").GetValue(listItem), 
                                listItem.GetType().GetProperty("PurchaseDate").GetValue(listItem));
                            purchaseCount++;
                            break;
                        case "Review":
                            caption = String.Format("Review - {0}", listItem.GetType().GetProperty("ReviewDate").GetValue(listItem).ToString());
                            value = listItem.GetType().GetProperty("Comment").GetValue(listItem);
                            break;
                        default:
                            caption = "Not Relfected List";
                            value = listItem.GetType().Name;
                            break;
                    }
                    //CANNOT HAVE duplicate keys
                    //If you have a list that you use the same caption you will want to 
                    //implement something like a counter and add that number to it.
                    if (!valueObject.ContainsKey(caption))
                    {
                        valueObject.Add(caption, value);
                    }
                }
            }
            return valueObject;
        }

        public IDictionary<string, object> ReflectICollection(PropertyInfo propInfo, object parentObject)
        {
            //This will basically work like the GenericList one
            var valueObject = new ExpandoObject() as IDictionary<string, object>;
            ICollection itemList = propInfo.GetValue(parentObject, null) as ICollection;
            if (itemList != null)
            {
                foreach (var listItem in itemList)
                {
                    object value = new object();
                    string caption = String.Empty;

                    switch (listItem.GetType().Name)
                    {
                        case "SomeList":
                            break;
                        default:
                            break;
                    }
                    //CANNOT HAVE duplicate keys
                    //If you have a list that you use the same caption you will want to 
                    //implement something like a counter and add that number to it.
                    if (!valueObject.ContainsKey(caption))
                    {
                        valueObject.Add(caption, value);
                    }
                }
            }
            return valueObject;
        }

    }
}