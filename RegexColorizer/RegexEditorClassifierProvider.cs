using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace RegexColorizer
{
    [Export(typeof(IClassifierProvider))]
    [ContentType("regex")]
    internal class RegexEditorClassifierProvider : IClassifierProvider
    {
        [Import]
        internal IClassificationTypeRegistryService ClassificationRegistry;

        private static RegexEditorClassifier _classifier;

        public IClassifier GetClassifier(ITextBuffer buffer)
        {
            //return _classifier ?? (_classifier = new RegexEditorClassifier(ClassificationRegistry));
            return buffer.Properties.GetOrCreateSingletonProperty(() => new RegexEditorClassifier(ClassificationRegistry));
        }
    }
}
