using UnityEngine;
namespace ClientCore {
    public interface LoadBundleInterface {
        void IAddBundleForDownload(string bundleURL, string assetName, int versionNumber);
        bool IIsDownloadFinished();
        AssetBundle IGetAssetBundle(string bundleName);
    }
}
