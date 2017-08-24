﻿// This file is part of TorrentCore.
//   https://torrentcore.org
// Copyright (c) Samuel Fisher.
//
// Licensed under the GNU Lesser General Public License, version 3. See the
// LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorrentCore.Application.BitTorrent.Messages
{
    /// <summary>
    /// A message used to inform a peer that it is being unchocked.
    /// </summary>
    public class UnchokeMessage : CommonPeerMessage
    {
        public const byte MessageID = 1;

        /// <summary>
        /// The ID of the message.
        /// </summary>
        public override byte ID
        {
            get { return MessageID; }
        }
    }
}
