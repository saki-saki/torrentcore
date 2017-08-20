﻿// This file is part of TorrentCore.
//     https://torrentcore.org
// Copyright (c) 2016 Sam Fisher.
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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorrentCore.Application.BitTorrent.Messages
{
    /// <summary>
    /// Represents a message with an ID and length sent to or received from a remote peer.
    /// </summary>
    public abstract class CommonPeerMessage : IPeerMessage
    {
        /// <summary>
        /// Gets the ID of the message.
        /// </summary>
        public abstract byte ID { get; }

        /// <summary>
        /// Sends the message by writing it to the specified BinaryWriter.
        /// </summary>
        /// <param name="writer">The writer to use.</param>
        public virtual void Send(BinaryWriter writer)
        {
            // Message ID
            writer.Write(ID);

            // Store to send
            writer.Flush();
        }

        /// <summary>
        /// Receives a message by reading it from the specified reader.
        /// <remarks>The length and ID of the message have already been read.</remarks>
        /// </summary>
        /// <param name="reader">The reader to use.</param>
        /// <param name="length">The length of the message, in bytes.</param>
        public virtual void Receive(BinaryReader reader, int length)
        {
            // Do nothing
        }
    }
}
