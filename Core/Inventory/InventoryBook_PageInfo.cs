using System.Collections.Generic;
using Terraria;
using Core.Inventory;


namespace Core.Inventory {
	partial class InventoryBook {
		public Item[] GetPageItems( Player player, int pageNum ) {
			if( pageNum == this.CurrentPageIdx ) {
				return player.inventory;
			} else {
				return this.Pages[ pageNum ].Items;
			}
		}

		public IList<InventoryPage> GetSharingPages( out bool includesCurrent ) {
			var pages = new List<InventoryPage>( this.Pages.Count );
			includesCurrent = false;

			for( int i=0; i<this.Pages.Count; i++ ) {
				InventoryPage page = this.Pages[i];

				if( page.IsSharing ) {
					if( i == this.CurrentPageIdx ) {
						includesCurrent = true;
					}
					pages.Add( page );
				}
			}

			return pages;
		}
		
		
		////////////////

		public string PagePositionToString() {
			return ( this.CurrentPageIdx + 1 ) + " / " + this.Pages.Count;
		}
	}
}
