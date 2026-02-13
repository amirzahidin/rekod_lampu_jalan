package mono.com.google.maps.android.clustering;


public class ClusterManager_OnClusterInfoWindowLongClickListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.maps.android.clustering.ClusterManager.OnClusterInfoWindowLongClickListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onClusterInfoWindowLongClick:(Lcom/google/maps/android/clustering/Cluster;)V:GetOnClusterInfoWindowLongClick_Lcom_google_maps_android_clustering_Cluster_Handler:Android.Gms.Maps.Utils.Clustering.ClusterManager/IOnClusterInfoWindowLongClickListenerInvoker, Google.Maps.Utils.Android\n" +
			"";
		mono.android.Runtime.register ("Android.Gms.Maps.Utils.Clustering.ClusterManager+IOnClusterInfoWindowLongClickListenerImplementor, Google.Maps.Utils.Android", ClusterManager_OnClusterInfoWindowLongClickListenerImplementor.class, __md_methods);
	}

	public ClusterManager_OnClusterInfoWindowLongClickListenerImplementor ()
	{
		super ();
		if (getClass () == ClusterManager_OnClusterInfoWindowLongClickListenerImplementor.class) {
			mono.android.TypeManager.Activate ("Android.Gms.Maps.Utils.Clustering.ClusterManager+IOnClusterInfoWindowLongClickListenerImplementor, Google.Maps.Utils.Android", "", this, new java.lang.Object[] {  });
		}
	}

	public void onClusterInfoWindowLongClick (com.google.maps.android.clustering.Cluster p0)
	{
		n_onClusterInfoWindowLongClick (p0);
	}

	private native void n_onClusterInfoWindowLongClick (com.google.maps.android.clustering.Cluster p0);

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
