using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using PodioAPI.Models;

namespace PodioAPI.Utils.ItemFields
{
    public class EmbedItemField : ItemField
    {
        private List<Embed> _embeds;

        public IEnumerable<Embed> Embeds
        {
            get
            {
                if (_embeds == null)
                {
                    _embeds = new List<Embed>();
                    if (this.Values != null)
                    {
                        foreach (var embedFilePair in this.Values)
                        {
                            var embed = this.ValueAs<Embed>(embedFilePair, "embed");
                            if (embedFilePair["file"] != null)
                            {
                                var file = this.ValueAs<FileAttachment>(embedFilePair, "file");
                                if (embed.Files == null)
                                {
                                    embed.Files = new List<FileAttachment>();
                                }
                                embed.Files.Add(file);
                            }
                            _embeds.Add(embed);
                        }
                    }
                }
                return _embeds;
            }
        }

        public void AddEmbed(int embedId, int? fileId = null)
        {
            EnsureValuesInitialized();
            var jobject = new JObject();
            jobject["embed"] = embedId;
            if (fileId != null)
            {
                jobject["file"] = fileId;
            }
            this.Values.Add(jobject);
        }
    }
}