using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace RegexColorizer
{
    internal static class ClassificationDefinitions
    {
        [Export]
        [Name("regex")]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition RegexContentTypeDefinition;

        [Export]
        [FileExtension(".regex")]
        [ContentType("regex")]
        internal static FileExtensionToContentTypeDefinition RegexFileExtensionDefinition;

        [Export]
        [Name("RegexDot")]
        [BaseDefinition("text")]
        internal static ClassificationTypeDefinition RegexDotDefinition;

        [Export]
        [Name("RegexComment")]
        [BaseDefinition("text")]
        internal static ClassificationTypeDefinition RegexCommentDefinition;

        [Export]
        [Name("RegexCharacterClass")]
        [BaseDefinition("text")]
        internal static ClassificationTypeDefinition RegexCharacterClassDefinition;

        [Export]
        [Name("RegexGroup")]
        [BaseDefinition("text")]
        internal static ClassificationTypeDefinition RegexGroupDefinition;

        [Export]
        [Name("RegexBackreference")]
        [BaseDefinition("text")]
        internal static ClassificationTypeDefinition RegexBackreferenceDefinition;

        [Export]
        [Name("RegexMetasequence")]
        [BaseDefinition("text")]
        internal static ClassificationTypeDefinition RegexMetasequenceDefinition;

        [Export]
        [Name("RegexError")]
        [BaseDefinition("text")]
        internal static ClassificationTypeDefinition RegexErrorDefinition;

        [Export]
        [Name("RegexEscape")]
        [BaseDefinition("text")]
        internal static ClassificationTypeDefinition RegexEscapeDefinition;

        [Export]
        [Name("RegexQuantifier")]
        [BaseDefinition("text")]
        internal static ClassificationTypeDefinition RegexQuantifierDefinition;

        [Export]
        [Name("RegexAlternator")]
        [BaseDefinition("text")]
        internal static ClassificationTypeDefinition RegexAlternatorDefinition;

        [Export]
        [Name("RegexAnchor")]
        [BaseDefinition("text")]
        internal static ClassificationTypeDefinition RegexAnchorDefinition;

        [Export(typeof(EditorFormatDefinition))]
        [ClassificationType(ClassificationTypeNames = "RegexDot")]
        [Name("RegexDot")]
        internal sealed class RegexDotFormat : ClassificationFormatDefinition
        {
            public RegexDotFormat()
            {
                IsBold = true;
                BackgroundColor = ColorConverter.ConvertFromString("#aad1f7") as Color?;
            }
        }

        [Export(typeof(EditorFormatDefinition))]
        [ClassificationType(ClassificationTypeNames = "RegexComment")]
        [Name("RegexComment")]
        internal sealed class RegexCommentFormat : ClassificationFormatDefinition
        {
            public RegexCommentFormat()
            {
                //var sb = new StringBuilder();
                //var type = typeof(EnvironmentColors);

                //foreach (var key in type.GetProperties(BindingFlags.Static | BindingFlags.Public))
                //{
                //    if (key.PropertyType != typeof(ThemeResourceKey)) continue;
                //    var keyValue = key.GetValue(null);
                //    var color = VSColorTheme.GetThemedColor((ThemeResourceKey)keyValue);
                //    sb.Append($"<div style=\"color:#{color.R:X2}{color.G:X2}{color.B:X2}\">{key.Name}</div>");
                //}

                //var outp = sb.ToString();

                ForegroundColor = Colors.Green;
                //VSColorTheme.GetThemedColor(EnvironmentColors.).ToMediaColor();
            }
        }

        [Export(typeof(EditorFormatDefinition))]
        [ClassificationType(ClassificationTypeNames = "RegexCharacterClass")]
        [Name("RegexCharacterClass")]
        internal sealed class RegexCharacterClassFormat : ClassificationFormatDefinition
        {
            public RegexCharacterClassFormat()
            {
                ForegroundColor = Colors.Orange;
            }
        }

        [Export(typeof(EditorFormatDefinition))]
        [ClassificationType(ClassificationTypeNames = "RegexGroup")]
        [Name("RegexGroup")]
        internal sealed class RegexGroupFormat : ClassificationFormatDefinition
        {
            public RegexGroupFormat()
            {
                ForegroundColor = Colors.LightGreen;
            }
        }

        [Export(typeof(EditorFormatDefinition))]
        [ClassificationType(ClassificationTypeNames = "RegexBackreference")]
        [Name("RegexBackreference")]
        internal sealed class RegexBackreferenceFormat : ClassificationFormatDefinition
        {
            public RegexBackreferenceFormat()
            {
                ForegroundColor = Colors.LightBlue;
            }
        }

        [Export(typeof(EditorFormatDefinition))]
        [ClassificationType(ClassificationTypeNames = "RegexMetasequence")]
        [Name("RegexMetasequence")]
        internal sealed class RegexMetasequenceFormat : ClassificationFormatDefinition
        {
            public RegexMetasequenceFormat()
            {
                ForegroundColor = Colors.LightCoral;
            }
        }

        [Export(typeof(EditorFormatDefinition))]
        [ClassificationType(ClassificationTypeNames = "RegexError")]
        [Name("RegexError")]
        internal sealed class RegexErrorFormat : ClassificationFormatDefinition
        {
            public RegexErrorFormat()
            {
                ForegroundColor = Colors.Red;
            }
        }

        [Export(typeof(EditorFormatDefinition))]
        [ClassificationType(ClassificationTypeNames = "Escape")]
        [Name("Escape")]
        internal sealed class EscapeFormat : ClassificationFormatDefinition
        {
            public EscapeFormat()
            {
                ForegroundColor = Colors.LightGoldenrodYellow;
            }
        }

        [Export(typeof(EditorFormatDefinition))]
        [ClassificationType(ClassificationTypeNames = "RegexQuantifier")]
        [Name("RegexQuantifier")]
        internal sealed class RegexQuantifierFormat : ClassificationFormatDefinition
        {
            public RegexQuantifierFormat()
            {
                ForegroundColor = Colors.LightSeaGreen;
            }
        }

        [Export(typeof(EditorFormatDefinition))]
        [ClassificationType(ClassificationTypeNames = "RegexAlternator")]
        [Name("RegexAlternator")]
        internal sealed class RegexAlternatorFormat : ClassificationFormatDefinition
        {
            public RegexAlternatorFormat()
            {
                ForegroundColor = Colors.Gray;
            }
        }

        [Export(typeof(EditorFormatDefinition))]
        [ClassificationType(ClassificationTypeNames = "RegexAnchor")]
        [Name("RegexAnchor")]
        internal sealed class RegexAnchorFormat : ClassificationFormatDefinition
        {
            public RegexAnchorFormat()
            {
                ForegroundColor = Colors.Brown;
            }
        }

    }
}