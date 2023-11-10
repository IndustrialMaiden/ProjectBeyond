using System.Collections.Generic;
using _CONTENT.CodeBase.Unity_delaunay.utils.Utils;

namespace _CONTENT.CodeBase.Unity_delaunay.Delaunay
{
	
	public sealed class Triangle: IDisposable
	{
		private List<Site> _sites;
		public List<Site> sites {
			get { return this._sites; }
		}
		
		public Triangle (Site a, Site b, Site c)
		{
			_sites = new List<Site> () { a, b, c };
		}
		
		public void Dispose ()
		{
			_sites.Clear ();
			_sites = null;
		}

	}
}