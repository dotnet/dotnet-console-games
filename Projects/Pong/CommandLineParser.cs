// Thanks to wertrain@github.com
// Original source code: https://github.com/wertrain/command-line-parser-cs (Version 0.1)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CommandLineParser
{
    /// <summary>
    /// Option attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class Option : Attribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Option()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="longName"></param>
        public Option(string longName)
        {
            LongName = longName;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="shortName"></param>
        public Option(char shortName)
        {
            ShortName = shortName;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="shortName"></param>
        /// <param name="longName"></param>
        public Option(char shortName, string longName)
        {
            ShortName = shortName;
            LongName = longName;
        }

        /// <summary>
        /// long name command option
        /// </summary>
        public string LongName { get; private set; }

        /// <summary>
        /// short name command option
        /// </summary>
        public char ShortName { get; private set; }

        /// <summary>
        /// command option
        /// </summary>
        public string HelpText { get; set; }

        /// <summary>
        /// command option
        /// </summary>
        public bool Required { get; set; }
    }

    /// <summary>
    /// Results type of parse
    /// </summary>
    public enum ParserResultType
    {
        /// <summary>
        /// 
        /// </summary>
        Parsed = 0,

        /// <summary>
        /// 
        /// </summary>
        NotParsed
    }

    /// <summary>
    /// Results of parse
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ParserResult<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag"></param>
        internal ParserResult(ParserResultType tag, T value)
        {
            Tag = tag;
            Value = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public ParserResultType Tag { get; }

        /// <summary>
        /// 
        /// </summary>
        public T Value { get; }
    }

    /// <summary>
    /// Utility for parser
    /// </summary>
    static class ParserUtility
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static T ConvertValue<T>(this string input)
        {
            var converter = System.ComponentModel.TypeDescriptor.GetConverter(typeof(T));
            if (converter != null)
            {
                return (T)converter.ConvertFromString(input);
            }
            return default(T);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static bool SetValueToProperty<T>(PropertyInfo property, T value, string param)
        {
            switch (property.PropertyType.Name)
            {
                case "Boolean":
                    if (string.IsNullOrEmpty(param))
                    {
                        property.SetValue(value, true);
                    }
                    else
                    {
                        switch (param.ToLower())
                        {
                            case "true": property.SetValue(value, true); return true;
                            case "false": property.SetValue(value, false); return true;
                        }
                    }
                    return false;

                case "Byte": property.SetValue(value, ConvertValue<byte>(param)); break;
                case "SByte": property.SetValue(value, ConvertValue<sbyte>(param)); break;
                case "Char": property.SetValue(value, ConvertValue<char>(param)); break;
                case "Decimal": property.SetValue(value, ConvertValue<decimal>(param)); break;
                case "Double": property.SetValue(value, ConvertValue<double>(param)); break;
                case "Single": property.SetValue(value, ConvertValue<float>(param)); break;
                case "Int32": property.SetValue(value, ConvertValue<int>(param)); break;
                case "UInt32": property.SetValue(value, ConvertValue<uint>(param)); break;
                case "Int64": property.SetValue(value, ConvertValue<long>(param)); break;
                case "UInt64": property.SetValue(value, ConvertValue<ulong>(param)); break;
                case "Int16": property.SetValue(value, ConvertValue<short>(param)); break;
                case "UInt16": property.SetValue(value, ConvertValue<ushort>(param)); break;

                case "String":
                    property.SetValue(value, param);
                    return !string.IsNullOrEmpty(param);
            } 
            return true;
        }
    }

    /// <summary>
    /// Arguments parser
    /// </summary>
    public class Parser
    {
        /// <summary>
        /// Help command
        /// </summary>
        private const string HelpLongName = "help";

        /// <summary>
        /// Parse command line
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="args"></param>
        /// <returns></returns>
        public static ParserResult<T> Parse<T>(string args) where T : new()
        {
            var enumerable = args.Split();
            return Parse<T>(enumerable);
        }

        /// <summary>
        /// Parse command line
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="args"></param>
        /// <returns></returns>
        public static ParserResult<T> Parse<T>(IEnumerable<string> args) where T : new()
        {
            if (args.Count() == 0)
            {
                ShowHelp<T>();
                return new ParserResult<T>(ParserResultType.NotParsed, new T());
            }

            var longNameDictionary = new Dictionary<string, Tuple<string, Option>>();
            var shortNameDictionary = new Dictionary<char, Tuple<string, Option>>();

            foreach (var property in typeof(T).GetProperties())
            {
                var attributes = Attribute.GetCustomAttributes(property, typeof(Option));

                foreach (Option option in attributes)
                {
                    if (!string.IsNullOrWhiteSpace(option.LongName))
                    {
                        longNameDictionary.Add(option.LongName, new Tuple<string, Option>(property.Name, option));
                    }

                    if (option.ShortName != '\0')
                    {
                        shortNameDictionary.Add(option.ShortName, new Tuple<string, Option>(property.Name, option));
                    }
                }
            }

            var value = new T();
            var resultTag = ParserResultType.Parsed;

            for (int i = 0, max = args.Count(); i < max; ++i)
            {
                var arg = args.ElementAt(i);
                Tuple<string, Option> pair = null;

                if (arg.StartsWith("--"))
                {
                    string command = arg.Substring(2);

                    if (command == HelpLongName)
                    {
                        ShowHelp<T>();

                        return new ParserResult<T>(ParserResultType.Parsed, new T());
                    }

                    if (longNameDictionary.ContainsKey(command))
                    {
                        pair = longNameDictionary[command];
                    }
                }
                else if (arg.StartsWith("-"))
                {
                    string command = arg.Substring(1);

                    if (command.Length != 1)
                    {
                        continue;
                    }

                    if (shortNameDictionary.ContainsKey(command[0]))
                    {
                        pair = shortNameDictionary[command[0]];
                    }
                }

                if (pair == null)
                {
                    continue;
                }

                foreach (var property in typeof(T).GetProperties())
                {
                    if (pair.Item1 == property.Name)
                    {
                        string param = string.Empty;

                        if (args.Count() > i + 1)
                        {
                            param = args.ElementAt(i + 1);
                        }

                        try
                        {
                            if (ParserUtility.SetValueToProperty<T>(property, value, param))
                            {
                                ++i;
                            }
                        }
                        catch
                        {
                            resultTag = ParserResultType.NotParsed;
                        }
                    }

                }
            }

            return new ParserResult<T>(resultTag, value);
        }

        /// <summary>
        /// Show help
        /// </summary>
        public static void ShowHelp<T>()
        {
            int SpacePaddingSize = 12;

            var asm = Assembly.GetExecutingAssembly();
            Console.WriteLine("{0} {1}", System.IO.Path.GetFileNameWithoutExtension(asm.Location), asm.GetName().Version);

            foreach (var property in typeof(T).GetProperties())
            {
                var attributes = Attribute.GetCustomAttributes(property, typeof(Option));
                foreach (Option attribute in attributes)
                {
                    Console.Write("-{0}, --{1,-" + SpacePaddingSize + "} ", attribute.ShortName, attribute.LongName);

                    if (attribute.Required)
                    {
                        Console.Write("[Required]");
                    }

                    Console.WriteLine("{0}", attribute.HelpText);
                }
            }

            Console.WriteLine("--{0,-" + (SpacePaddingSize + HelpLongName.Length) + "} {1}", HelpLongName, "Display this help screen.");
        }
    }
}