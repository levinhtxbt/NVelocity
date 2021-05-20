using NVelocity.Context;
using NVelocity.Runtime.Parser.Node;
using System;
using System.IO;
using NVelocity.Tool;
using Microsoft.Extensions.Logging;

namespace NVelocity.Runtime.Directive
{
	public class TemplateDirective : Directive
	{
		private readonly ITemplateLoader _templateLoader;
		private readonly ILogger<TemplateDirective> _logger;

		public TemplateDirective(ITemplateLoader templateLoader, ILogger<TemplateDirective> logger)
		{
			_templateLoader = templateLoader;
			_logger = logger;
		}
			

		public override string Name { get => "template"; set => throw new NotSupportedException(); }

		public override DirectiveType Type { get => DirectiveType.LINE; }

		public override bool Render(IInternalContextAdapter context, TextWriter writer, INode node)
		{
			try
			{
				var myWriter = new StringWriter();

				var child = node?.GetChild(0);

				if (child != null)
				{
					var name = child.Value(context).ToString();

					_logger.LogInformation($"Template name: {name}");

					var html = _templateLoader.GetTemplate(name);

					if (string.IsNullOrEmpty(html))
						_logger.LogInformation("Html is null or empty");
					else
						_logger.LogInformation(html);

					var template = new StringTemplate(html);

					template.runtimeServices = runtimeServices;

					if (template.Process())
					{
						if ((SimpleNode)template.Data == null)
							_logger.LogError("SimpleNode Template Data is null");

						((SimpleNode)template.Data).Render(context, myWriter);
					}	

					writer.WriteLine(myWriter.ToString());

					return true;
				}
			}catch(System.Exception ex)
			{
				_logger.LogError(ex.Message);
			}

			return false;
		}
	}
}
