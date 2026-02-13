package mono.com.google.maps.android.clustering;


public class ClusterManager_OnClusterItemInfoWindowClickListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.maps.android.clustering.ClusterManager.OnClusterItemInfoWindowClickListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onClusterItemInfoWindowClick:(Lcom/google/maps/android/clustering/ClusterItem;)V:GetOnClusterItemInfoWindowClick_Lcom_google_maps_android_clustering_ClusterItem_Handler:Android.Gms.Maps.Utils.Clustering.ClusterManager/IOnClusterItemInfoWindowClickListenerInvoker, Google.Maps.Utils.Android\n" +
			"";
		mono.android.Runtime.register ("Android.Gms.Maps.Utils.Clustering.ClusterManager+IOnClusterItemInfoWindowClickListenerImplementor, Google.Maps.Utils.Android", ClusterManager_OnClusterItemInfoWindowClickListenerImplementor.class, __md_methods);
	}

	public ClusterManager_OnClusterItemInfoWindowClickListenerImplementor ()
	{
		super ();
		if (getClass () == ClusterManager_OnClusterItemInfoWindowClickListenerImplementor.class) {
			mono.android.TypeManager.Activate ("Android.Gms.Maps.Utils.Clustering.ClusterManager+IOnClusterItemInfoWindowClickListenerImplementor, Google.Maps.Utils.Android", "", this, new java.lang.Object[] {  });
		}
	}

	public void onClusterItemInfoWindowClick (com.google.maps.android.clustering.ClusterItem p0)
	{
		n_onClusterItemInfoWindowClick (p0);
	}

	private native void n_onClusterItemInfoWindowClick (com.google.maps.android.clustering.ClusterItem p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
