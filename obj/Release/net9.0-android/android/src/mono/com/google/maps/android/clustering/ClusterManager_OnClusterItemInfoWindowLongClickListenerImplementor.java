package mono.com.google.maps.android.clustering;


public class ClusterManager_OnClusterItemInfoWindowLongClickListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.maps.android.clustering.ClusterManager.OnClusterItemInfoWindowLongClickListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onClusterItemInfoWindowLongClick:(Lcom/google/maps/android/clustering/ClusterItem;)V:GetOnClusterItemInfoWindowLongClick_Lcom_google_maps_android_clustering_ClusterItem_Handler:Android.Gms.Maps.Utils.Clustering.ClusterManager/IOnClusterItemInfoWindowLongClickListenerInvoker, Google.Maps.Utils.Android\n" +
			"";
		mono.android.Runtime.register ("Android.Gms.Maps.Utils.Clustering.ClusterManager+IOnClusterItemInfoWindowLongClickListenerImplementor, Google.Maps.Utils.Android", ClusterManager_OnClusterItemInfoWindowLongClickListenerImplementor.class, __md_methods);
	}

	public ClusterManager_OnClusterItemInfoWindowLongClickListenerImplementor ()
	{
		super ();
		if (getClass () == ClusterManager_OnClusterItemInfoWindowLongClickListenerImplementor.class) {
			mono.android.TypeManager.Activate ("Android.Gms.Maps.Utils.Clustering.ClusterManager+IOnClusterItemInfoWindowLongClickListenerImplementor, Google.Maps.Utils.Android", "", this, new java.lang.Object[] {  });
		}
	}

	public void onClusterItemInfoWindowLongClick (com.google.maps.android.clustering.ClusterItem p0)
	{
		n_onClusterItemInfoWindowLongClick (p0);
	}

	private native void n_onClusterItemInfoWindowLongClick (com.google.maps.android.clustering.ClusterItem p0);

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
