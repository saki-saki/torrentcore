﻿// This file is part of TorrentCore.
//     https://torrentcore.org
// Copyright (c) 2017 Sam Fisher.
// 
// TorrentCore is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, version 3.
// 
// TorrentCore is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with TorrentCore.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TorrentCore.Application.BitTorrent;
using TorrentCore.Transport;
using TorrentCore.Transport.Tcp;

namespace TorrentCore.Web.Controllers
{
    [Route("api/[controller]")]
    public class PeersController : Controller
    {
        private readonly TorrentClient client;

        public PeersController(TorrentClient client)
        {
            this.client = client;
        }

        [HttpGet]
        public IList<PeerDetails> Get()
        {
            return client.Downloads.SelectMany(x => x.Manager.ApplicationProtocol.Peers).Select(x => new PeerDetails(x)).ToList();
        }

        [HttpGet("{peerId}")]
        public PeerDetails GetPeer(string peerId)
        {
            return Get().First(x => x.PeerId == peerId);
        }

        public class PeerDetails
        {
            internal PeerDetails(PeerConnection x)
            {
                Address = x.Address;
                PeerId = Convert.ToBase64String(x.PeerId.Value.ToArray());
                Client = x.PeerId.ClientName;
                ClientVersion = x.PeerId.ClientVersion;
                string ipAddress = Address.Split(':').First();
                SupportedExtensions = GetProtocolExtensions(x.SupportedExtensions);

                try
                {
                    var hostEntry = Dns.GetHostEntryAsync(ipAddress).Result;
                    Host = hostEntry.HostName;
                }
                catch
                {
                    Host = ipAddress;
                }
            }

            public string Address { get; }
            public string Host { get; }
            public string PeerId { get; }
            public string Client { get; }
            public int? ClientVersion { get; }
            public IReadOnlyList<string> SupportedExtensions { get; }

            private List<string> GetProtocolExtensions(ProtocolExtension extensions)
            {
                var results = new List<string>();
                foreach (ProtocolExtension e in Enum.GetValues(typeof(ProtocolExtension)))
                {
                    if ((extensions & e) != 0)
                        results.Add(e.ToString());
                }
                return results;
            }
        }
    }
}
