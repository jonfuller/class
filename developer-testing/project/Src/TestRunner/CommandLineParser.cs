﻿using System;
using System.Collections.Generic;

namespace Runner
{
    /// <summary>
    /// CommandLineParser
    /// Version 1, 2008-06-22
    /// http://www.Stum.de
    /// 
    /// A static class to parse a command line.
    /// </summary>
    /// <remarks>
    /// Licensed under WTFPL
    /// http://sam.zoy.org/wtfpl/
    /// </remarks>
    public static class CommandLineParser
    {
        /// <summary>
        /// Transform the command line string array into a Dictionary.
        /// </summary>
        /// <param name="args">The string array, usually string[] args from your main function</param>
        /// <returns>A Dictionary where the arguments are the keys and parameters to the arguments are in a List. Keep in mind that the List may be null!</returns>
        public static Dictionary<string, List<string>> ParseCommandLine(string[] args)
        {
            return ParseCommandLine(args, true, false);
        }

        /// <summary>
        /// Transform the command line string array into a Dictionary.
        /// </summary>
        /// <param name="args">The string array, usually string[] args from your main function</param>
        /// <param name="ignoreArgumentCase">Ignore the case of arguments? (if set to false, then "/beta" and "/Beta" are two different parameters</param>
        /// <returns>A Dictionary where the arguments are the keys and parameters to the arguments are in a List. Keep in mind that the List may be null!</returns>
        public static Dictionary<string, List<string>> ParseCommandLine(string[] args, bool ignoreArgumentCase)
        {
            return ParseCommandLine(args, ignoreArgumentCase, false);
        }

        /// <summary>
        /// Transform the command line string array into a Dictionary.
        /// </summary>
        /// <param name="args">The string array, usually string[] args from your main function</param>
        /// <param name="ignoreArgumentCase">Ignore the case of arguments? (if set to false, then "/beta" and "/Beta" are two different arguments)</param>
        /// <param name="allowMultipleParameters">Allow multiple parameters to one argument.</param>
        /// <returns>A Dictionary where the arguments are the keys and parameters to the arguments are in a List. Keep in mind that the List may be null!</returns>
        /// <remarks>
        /// If allowMultipleParameters is set to true, then "/delta omega kappa" will cause omega and kappa to be two parameters to the argument delta.
        /// If allowMultipleParameters is set to false, then omega will be a parameter to delta, but kappa will be assigned to string.Empty.
        /// </remarks>
        public static Dictionary<string, List<string>> ParseCommandLine(string[] args, bool ignoreArgumentCase, bool allowMultipleParameters)
        {
            Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();
            string currentArgument = string.Empty;

            for (int i = 0; i < args.Length; i++)
            {
                // Is this an argument?
                if ((args[i].StartsWith("-", StringComparison.OrdinalIgnoreCase) || args[i].StartsWith("/", StringComparison.OrdinalIgnoreCase)) && args[i].Length > 1)
                {
                    currentArgument = args[i].Remove(0, 1);
                    if (ignoreArgumentCase)
                    {
                        currentArgument = currentArgument.ToLowerInvariant();
                    }
                    if (!result.ContainsKey(currentArgument))
                    {
                        result.Add(currentArgument, null);
                    }
                }
                else // No, it's a parameter
                {
                    List<string> paramValues = null;
                    if (result.ContainsKey(currentArgument))
                    {
                        paramValues = result[currentArgument];
                    }
                    if (paramValues == null)
                    {
                        paramValues = new List<string>();
                    }
                    paramValues.Add(args[i]);
                    result[currentArgument] = paramValues;
                    if (!allowMultipleParameters)
                    {
                        currentArgument = string.Empty;
                    }
                }
            }
            return result;
        }
    }
}