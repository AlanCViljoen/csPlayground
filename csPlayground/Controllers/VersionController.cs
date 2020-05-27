using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace csPlayground.Controllers
{
	/// <summary>
	/// Version Controller
	/// </summary>
	[ApiController]
	[Route("api/[controller]")]
	public class VersionController : ControllerBase
	{
		private readonly ILogger<VersionController> _logger;

		public VersionController(ILogger<VersionController> logger)
		{
			_logger = logger;
		}

		private string gitVersion
		{
			get
			{
				string gitVersion = String.Empty;
				using (Stream stream = Assembly.GetExecutingAssembly()
						.GetManifestResourceStream("csPlayground." + "version.txt"))
				using (StreamReader reader = new StreamReader(stream))
				{
					gitVersion = reader.ReadToEnd();
				}
				return gitVersion;
			}
		}

		/// <summary>
		/// Get Git Version
		/// </summary>
		/// <returns>Git Version running on system</returns>
		[HttpGet]
		public ContentResult GetVersion()
		{
			var versionText = $"[{ Assembly.GetExecutingAssembly().FullName }] Git Commit [{ gitVersion.Trim() }] Running on [{ Environment.MachineName }]";
			return new ContentResult
			{
				ContentType = "text",
				StatusCode = (int)HttpStatusCode.OK,
				Content = versionText
			};
		}
	}
}
