using System;
using System.Collections.Generic;
using System.Reflection;

namespace EPGM.Framework
{
    public partial class SearchFilter
    {
        public string sidx { get; set; }
        public string sord { get; set; }
        public int page { get; set; }
        public int rows { get; set; }
        public bool _search { get; set; }
        public string searchField { get; set; }
        public string searchOper { get; set; }
        public string searchString { get; set; }
        public string filters { get; set; }
        public string AndRule { get; set; }
        public string OrRule { get; set; }

        static Dictionary<string, Tuple<string, string, string>> dataFields = new Dictionary<string, Tuple<string, string, string>>();
        public string GetCondition<T>(string ExtraAndCond = "")
        {

            var OrRuleList = new List<string>();
            if (!string.IsNullOrEmpty(this.OrRule))
            {
                foreach (var x in Newtonsoft.Json.JsonConvert.DeserializeObject<CustomRule>(this.OrRule).CustomRules)
                    OrRuleList.Add(GetCondition<T>(x));
            }
            var AndRuleList = new List<string>();
            if (_search)
            {
                AndRuleList.Add(GetCondition<T>(new RuleModel { field = searchField, op = searchOper, data = searchString }));
            }
            if (!string.IsNullOrEmpty(this.AndRule))
            {
                foreach (var x in Newtonsoft.Json.JsonConvert.DeserializeObject<CustomRule>(AndRule).CustomRules)
                    AndRuleList.Add(GetCondition<T>(x));
            }
            if (ExtraAndCond != "")
                AndRuleList.Add(ExtraAndCond);

            string AndFinal = string.Join(" && ", AndRuleList);
            string OrFinal = OrRuleList.Count > 0 ? "(" + string.Join(" || ", OrRuleList) + ")" : "";

            if (AndFinal != "" && OrFinal != "")
                return string.Join(" && ", AndFinal, OrFinal);
            else
                return AndFinal + OrFinal;
        }


        static Dictionary<string, string> WhereOperation =
                      new Dictionary<string, string>
                {
                    { "eq", "{1} =  {2}({0})" },
                    { "ne", "{1} != {2}({0})" },
                    { "lt", "{1} <  {2}({0})" },
                    { "le", "{1} <= {2}({0})" },
                    { "gt", "{1} >  {2}({0})" },
                    { "ge", "{1} >= {2}({0})" },
                    { "bw", "{1}.StartsWith({2}({0}))" },
                    { "bn", "!{1}.StartsWith({2}({0}))" },
                    { "in", "" },
                    { "ni", "" },
                    { "ew", "{1}.EndsWith({2}({0}))" },
                    { "en", "!{1}.EndsWith({2}({0}))" },
                    { "cn", "{1}.Contains({2}({0}))" },
                    { "nc", "!{1}.Contains({2}({0}))" },
                    { "nu", "{1} == null" },
                    { "nn", "{1} != null" }
                };

        static Dictionary<string, string> ValidOperators =
                       new Dictionary<string, string>
                {
                    { "Object"   ,  "" }, 
                    { "Boolean"  ,  "eq:ne:" }, 
                    { "Char"     ,  "" }, 
                    { "String"   ,  "eq:ne:lt:le:gt:ge:bw:bn:cn:nc:" }, 
                    { "SByte"    ,  "" }, 
                    { "Byte"     ,  "eq:ne:lt:le:gt:ge:" },
                    { "Int16"    ,  "eq:ne:lt:le:gt:ge:" }, 
                    { "UInt16"   ,  "" }, 
                    { "Int32"    ,  "eq:ne:lt:le:gt:ge:" }, 
                    { "UInt32"   ,  "" }, 
                    { "Int64"    ,  "eq:ne:lt:le:gt:ge:" }, 
                    { "UInt64"   ,  "" }, 
                    { "Decimal"  ,  "eq:ne:lt:le:gt:ge:" }, 
                    { "Single"   ,  "eq:ne:lt:le:gt:ge:" }, 
                    { "Double"   ,  "eq:ne:lt:le:gt:ge:" }, 
                    { "DateTime" ,  "eq:ne:lt:le:gt:ge:" }, 
                    { "TimeSpan" ,  "" }, 
                    { "Guid"     ,  "" }
                };
        string GetCondition<T>(RuleModel rule)
        {
            if (rule.data == "%")
            {
                // returns an empty string when the data is ‘%’
                return "";
            }
            else
            {
                // initializing variables

                string myTypeName = string.Empty;
                string myTypeRawName = string.Empty;
                string myTypeFormat = string.Empty;
                Type TType = typeof(T);
                string fld = rule.field.Split('.')[0];
                if (!dataFields.ContainsKey(TType.ToString() + "::" + fld))
                {
                    Type myType = null;
                    PropertyInfo myPropInfo = TType.GetProperty(fld.Split('.')[0]);
                    int index = 0;
                    // navigating fields hierarchy
                    foreach (string wrkField in rule.field.Split('.'))
                    {
                        if (index > 0)
                        {
                            myPropInfo = myPropInfo.PropertyType.GetProperty(wrkField);
                        }
                        index++;
                    }
                    // property type and its name
                    myType = myPropInfo.PropertyType;

                    myTypeName = myPropInfo.PropertyType.Name;
                    myTypeRawName = myTypeName;
                    // handling ‘nullable’ fields
                    if (myTypeName.ToLower() == "nullable`1")
                    {
                        myType = Nullable.GetUnderlyingType(myPropInfo.PropertyType);
                        myTypeName = myType.Name + "?";
                        myTypeRawName = myType.Name;
                    }
                    myTypeFormat = myType.Name.ToLower();
                    dataFields.Add(TType.ToString() + "::" + fld, new Tuple<string, string, string>(myTypeFormat, myTypeName, myTypeRawName));
                }
                else
                {

                    var cachedField = dataFields[TType.ToString() + "::" + fld];
                    myTypeFormat = cachedField.Item1;
                    myTypeName = cachedField.Item2;
                    myTypeRawName = cachedField.Item3;
                }
                // creating the condition expression
                if (ValidOperators[myTypeRawName].Contains(rule.op + ":"))
                {
                    string expression = String.Format(WhereOperation[rule.op],
                                                      GetFormattedData(myTypeFormat, rule.data),
                                                      rule.field,
                                                      myTypeName);
                    return expression;
                }
                else
                {
                    // un-supported operator
                    return "";
                }
            }
        }
        string GetFormattedData(string typeformat, string value)
        {
            switch (typeformat)
            {
                case "string":
                    value = @"""" + value + @"""";
                    break;
                case "datetime":
                    DateTime dt = DateTime.Parse(value);
                    value = dt.Year.ToString() + "," +
                            dt.Month.ToString() + "," +
                            dt.Day.ToString();
                    break;
            }
            return value;
        }
    }


    public class CustomRule
    {
        public List<RuleModel> CustomRules { get; set; }
    }
    public class RuleModel
    {
        public string field { get; set; }
        public string op { get; set; }
        public string data { get; set; }
    }

}