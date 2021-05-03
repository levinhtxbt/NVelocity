using NVelocity.Context;
using NVelocity.Runtime.Parser.Node;
using System;
using System.IO;
using NVelocity.Tool;

namespace NVelocity.Runtime.Directive
{
	public class TemplateDirective : Directive
	{
		private readonly ITemplateLoader _templateLoader;

		public TemplateDirective(ITemplateLoader templateLoader)
			=> (_templateLoader) = (templateLoader);

		public override string Name { get => "template"; set => throw new NotSupportedException(); }

		public override DirectiveType Type { get => DirectiveType.LINE; }

		public override bool Render(IInternalContextAdapter context, TextWriter writer, INode node)
		{
			var myWriter = new StringWriter();

			var child = node?.GetChild(0);

			if(child != null)
			{
				var name = child.Value(context).ToString();

				var html = _templateLoader.GetTemplate(name);

				var template = new StringTemplate(html);

				template.runtimeServices = runtimeServices;

				if (template.Process())
					((SimpleNode)template.Data).Render(context, myWriter);

				writer.WriteLine(myWriter.ToString());

				return true;
			}

			return false;
		}
	}
}
