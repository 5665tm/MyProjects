// Changed 2014 08 30 1:01 PM Karavaev Vadim

using Newtonsoft.Json;

namespace VkApiDemo
{
	[JsonObject(MemberSerialization.OptIn)]
	internal struct Audio
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("artist")]
		public string Artist { get; set; }

		[JsonProperty("title")]
		public string Title { get; set; }
	}
}