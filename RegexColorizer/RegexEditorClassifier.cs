using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace RegexColorizer
{
    internal class RegexEditorClassifier : IClassifier
    {
        private readonly IClassificationTypeRegistryService _registry;
        //private readonly IClassificationType classificationType;

        private readonly Regex _commentToken = new Regex(@"^#.*$|[^\\]\#.*$", RegexOptions.Multiline);
        private readonly Regex _regexToken = new Regex(@"\[\^?]?(?:[^\\\]]+|\\[\S\s]?)*]?|\\(?:0(?:[0-3][0-7]{0,2}|[4-7][0-7]?)?|[1-9][0-9]*|x[0-9A-Fa-f]{2}|u[0-9A-Fa-f]{4}|c[A-Za-z]|[\S\s]?)|\((?:\?[:=!]?)?|(?:[?*+]|\{[0-9]+(?:,[0-9]*)?\})\??|[^.?*+^${[()|\\]+|.", RegexOptions.Multiline);
        //private readonly Regex _charClassToken = new Regex(@"[^\\-]+|-|\\(?:[0-3][0-7]{0,2}|[4-7][0-7]?|x[0-9A-Fa-f]{2}|u[0-9A-Fa-f]{4}|c[A-Za-z]|[\S\s]?)");
        //private readonly Regex _charClassParts = new Regex(@"^(\[\^?)(]?(?:[^\\\]]+|\\[\S\s]?)*)(]?)$");
        private readonly Regex _quantifier = new Regex(@"^^(?:[?*+]|\{[0-9]+(?:,[0-9]*)?\})\??$");

        internal RegexEditorClassifier(IClassificationTypeRegistryService registry)
        {
            _registry = registry;
        }

        #region IClassifier

#pragma warning disable 67
        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;
#pragma warning restore 67

        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            var snapshot = span.Snapshot;
            var result = new List<ClassificationSpan>();
            if (snapshot.Length == 0) return result;

            var text = span.GetText();

            foreach (Match match in _commentToken.Matches(text))
            {
                var index = match.Index;
                var length = match.Length;
                if (text[index] != '#')
                {
                    index++;
                    length--;
                }

                result.Add(new ClassificationSpan(new SnapshotSpan(snapshot, span.Start + index, length), _registry.GetClassificationType("RegexComment")));
                var s = new string(' ', length);
                text = text.Remove(index, length).Insert(index, s);
            }

            //var capturingGroupCount = 0;
            //var groupStyleDepth = 0;

            foreach (Match match in _regexToken.Matches(text))
            {
                var m = match.Value;
                var char0 = m[0];
                var char1 = m.Length > 1 ? m[1].ToString() : string.Empty;

                // Character class
                if (char0 == '[')
                {
                    result.Add(new ClassificationSpan(new SnapshotSpan(snapshot, span.Start + match.Index, match.Length), _registry.GetClassificationType("RegexCharacterClass")));
                }
                // Group opening
                else if (char0 == '(' || char0 == ')')
                {
                    result.Add(new ClassificationSpan(new SnapshotSpan(snapshot, span.Start + match.Index, match.Length), _registry.GetClassificationType("RegexGroup")));
                }
                //Escape or backreference
                else if (char0 == '\\')
                {
                    //Backreference or octal character code without a leading zero
                    if (new Regex("^[1-9]").IsMatch(char1))
                    {
                        result.Add(new ClassificationSpan(new SnapshotSpan(snapshot, span.Start + match.Index, match.Length), _registry.GetClassificationType("RegexBackreference")));
                    }
                    // Metasequence
                    else if (new Regex("^[0bBcdDfnrsStuvwWx]").IsMatch(char1))
                    {
                        result.Add(new ClassificationSpan(new SnapshotSpan(snapshot, span.Start + match.Index, match.Length), _registry.GetClassificationType("RegexMetasequence")));
                    }
                    // Unescaped "\" at the end of the regex
                    else if (m == "\\")
                    {
                        result.Add(new ClassificationSpan(new SnapshotSpan(snapshot, span.Start + match.Index, match.Length), _registry.GetClassificationType("RegexError")));
                    }
                    else
                    {
                        result.Add(new ClassificationSpan(new SnapshotSpan(snapshot, span.Start + match.Index, match.Length), _registry.GetClassificationType("RegexEscape")));
                    }
                }
                // Quantifier
                else if (_quantifier.IsMatch(m))
                {
                    result.Add(new ClassificationSpan(new SnapshotSpan(snapshot, span.Start + match.Index, match.Length), _registry.GetClassificationType("RegexQuantifier")));
                }
                // Vertical bar (alternator)
                else if (m == "|")
                {
                    result.Add(new ClassificationSpan(new SnapshotSpan(snapshot, span.Start + match.Index, match.Length), _registry.GetClassificationType("RegexAlternator")));
                }
                // ^ or $ anchor
                else if (m == "^" || m == "$")
                {
                    result.Add(new ClassificationSpan(new SnapshotSpan(snapshot, span.Start + match.Index, match.Length), _registry.GetClassificationType("RegexAnchor")));
                }
                // Dot (.)
                else if (m == ".")
                {
                    result.Add(new ClassificationSpan(new SnapshotSpan(snapshot, match.Index, match.Length), _registry.GetClassificationType("RegexDot")));
                }
            }

            return result;
        }

        #endregion
    }
}
