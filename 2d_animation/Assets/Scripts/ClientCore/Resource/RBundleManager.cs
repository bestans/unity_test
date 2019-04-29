using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using UnityEngine;

namespace ClientCore {
    /*
    public class Operation {
        public AsyncOperation ao;
        public AssetBundle bundle;
        public void Release() {
            if (null != bundle) {
                bundle.Unload(false);
            }
            bundle = null;
        }
        public bool IsDone() {
            return ao == null || ao.isDone;
        }
    }
    public class RBundleManager : TSingleton<RBundleManager> {

        public static class DownloadMsgCode {
            public const int CHECK_PATCH = 1;    //检测资源包，当前版本：v%1
            public const int UPDATE_FLINSH = 2;    //正在努力为指挥官加载（不消耗流量）
            public const int UPDATE_RES_SIZE = 3;    //共检测到 %1 M资源文件需要更新
            public const int UPDATE_RES_ING = 4;    //更新资源文件：%1
            public const int DOWNLOAD_PATCH_ERR = 5;    //下载补丁文件失败!
            public const int DOWNLOAD_PATCH_ING = 6;    //下载补丁文件%1
            public const int PATCH_INS_ERR = 7;   //补丁安装失败，请重新启动游戏再次尝试
            public const int DOWNLOAD_AFFICHE_ING = 8;   //正在加载公告
            public const int DOWNLOAD_AFFICHE_FAIL = 9;   //公告加载失败
            public const int DOWNLOAD_AFFICHE_SUCC = 10;   //公告加载完成
        }

        private RBundleManager() {
            CoroutineBehaviour = WWWLoadBundle.Singleton;
            m_CurrentPatchVersion = new PatchVersion(PathUtils.PatchVersionFile);
        }

        [Serializable]
        public class BundleChunk {
            public string m_Key = string.Empty;
            public string m_Md5 = string.Empty;
            public byte[] m_Data;
        }

        public class PakHeader {
            public string m_Version;
            public string m_Platform;
            public int m_Chunk;
        }

        /// <summary>
        /// 等待时间
        /// </summary>
        public const float WAIT_SECOND = 1f;
        /// <summary>
        /// 详细消息
        /// </summary>
        public string m_DetailMessage = string.Empty;
        /// <summary>
        /// 是否检查完更新
        /// </summary>
        public bool m_CompletedCheckUpdate;
        /// <summary>
        /// 是否下载完更新
        /// </summary>
        public bool m_CompletedDownloadUpdate;
        /// <summary>
        /// 是否暂停
        /// </summary>
        public bool m_IsPauseUpdate = false;
        /// <summary>
        /// 是否需要重启
        /// </summary>
        public bool m_IsNeedRestart = false;
        /// <summary>
        /// 当前下载
        /// </summary>
        public WWW m_CurrentWWWDownload;
        /// <summary>
        /// 当前更新版本文件
        /// </summary>
        private PatchVersion m_CurrentPatchVersion;
        /// <summary>
        /// 临时更新版本文件
        /// </summary>
        private PatchVersion m_TempPatchVersion;
        /// <summary>
        /// Chunk总数
        /// </summary>
        private int m_ChunkTotalCount = 0;
        /// <summary>
        /// 已安装的Chunk数
        /// </summary>
        private int m_ChunkSetupedCount = 0;
        /// <summary>
        /// 临时更新文件列表
        /// </summary>
        private List<string> m_TempPatchDownloadList = new List<string>();

        private MonoBehaviour m_CoroutineBehaviour = null;

        private Dictionary<string, UnityEngine.Object> m_BundleDict = new Dictionary<string, UnityEngine.Object>();
        private HashSet<string> m_BundlesManual;

        public MonoBehaviour CoroutineBehaviour {
            get { return m_CoroutineBehaviour; }
            set { m_CoroutineBehaviour = value; }
        }
        public HashSet<string> BundlesManual {
            get {
                if (m_BundlesManual == null) {
                    m_BundlesManual = new HashSet<string>();
                    string path = PathUtils.BundlesManualPath;
                    if (!File.Exists(path)) {
                        FileUtils.WriteAllText(path, string.Empty);
                    }
                    string[] array = File.ReadAllLines(path);
                    string[] tepArray = array;
                    for (int i = 0; i < tepArray.Length; i++) {
                        string item = tepArray[i];
                        m_BundlesManual.Add(item);
                    }
                }
                return m_BundlesManual;
            }
            set {
                string text = string.Empty;
                foreach (string current in value) {
                    text = text + current + Global.ENTER_STR;
                }
                FileUtils.WriteAllText(PathUtils.BundlesManualPath, text);
                m_BundlesManual = value;
            }
        }
        /// <summary>
        /// 显示下载消息
        /// </summary>
        /// <param name="wordId"></param>
        /// <param name="args"></param>
        private void ShowDownloadMessage(int wordId, params object[] args) {
            string resust = string.Empty;

            if (TableDB.Singleton.DownLoadMessageTable.ContainsRow(wordId))
                resust = TableDB.Singleton.DownLoadMessageTable[wordId].m_Content;

            m_DetailMessage = StringUtils.FormatString(resust, args);
        }

        private void OnDestroy() {
            m_BundleDict.Clear();
            m_BundleDict = null;
        }
        /// <summary>
        /// 检查更新包
        /// </summary>
        public void CheckPatch() {
            GlobalToolsFunction.Log("RbundleManager CheckPatch!");
            m_DetailMessage = string.Empty;
            m_CompletedCheckUpdate = false;
            m_CompletedDownloadUpdate = true;
            ShowDownloadMessage(DownloadMsgCode.CHECK_PATCH, Version.GetAppPatchVersion());
            CoroutineBehaviour.StartCoroutine(AsynCheckPatch());
        }
        /// <summary>
        /// 下载更新包
        /// </summary>
        public void DownloadPatch() {
            GlobalToolsFunction.Log("RbundleManager DownloadPatch!");
            m_DetailMessage = string.Empty;
            m_CompletedCheckUpdate = true;
            m_CompletedDownloadUpdate = false;

            if (m_TempPatchDownloadList.Count <= 0) {
                m_CompletedDownloadUpdate = true;
                ShowDownloadMessage(DownloadMsgCode.UPDATE_FLINSH);
                GlobalToolsFunction.Log("DownloadPatch completed!");
            } else {
                if (null != NetworkInterface.Singleton.GetWifiIP()) {
                    CoroutineBehaviour.StartCoroutine(AsynDownloadPatch());
                } else {
                    WMessageBox.Singleton.ShowWith(ClientCore.MESSAGEBOX_TYPE.E_HOT_UPDATE_WITHOUT_WIFI, _OnClickHotupdate);
                    return;
                }
            }
        }
        /// <summary>
        /// 下载单独配置
        /// </summary>
        public void DownloadSeparatelyConfigure() {
            CoroutineBehaviour.StartCoroutine(_AsynDownloadSeparatelyConfigure());
        }

        /// <summary>
        /// 是否点击过登陆
        /// </summary>
        public static bool IsAfficheAutoShow = false;

        /// <summary>
        /// 是否下载完server list
        /// </summary>
        public static bool IsServerListDownLoadDone = false;

        /// <summary>
        /// 下载单独配置
        /// </summary>
        private IEnumerator _AsynDownloadSeparatelyConfigure() {
            //download serverlist.tab
            yield return CoroutineBehaviour.StartCoroutine(DownloadServerList());
            yield return 2;

            TableDB.Singleton.LoadServerListTable();
            LoginPanel pPanel = UIManager.Singleton.GetWindow<LoginPanel>(UIWindowType.LOGIN_ACCOUNT);
            if (null != pPanel && pPanel.activeInHierarchy) {
                pPanel.SetCurrentServerID(GameRecord.System.LastServerID);
                pPanel.ShowEnterBtn();
            }
            IsServerListDownLoadDone = true;
 
            //download affiche
            yield return CoroutineBehaviour.StartCoroutine(DownloadAffiche());
            yield return 2;
            pPanel = UIManager.Singleton.GetWindow<LoginPanel>(UIWindowType.LOGIN_ACCOUNT);
            if (null != pPanel && pPanel.activeInHierarchy && !IsAfficheAutoShow) {
                string afficheFile = PathUtils.AfficheFile;
                if (!File.Exists(afficheFile)) {
                    GlobalToolsFunction.LogError("There is no affiche file " + afficheFile);
                }
                string affiche = FileUtils.ReadAllText(afficheFile);
                if (!string.IsNullOrEmpty(affiche)) {
                    ClientCore.UIManager.Singleton.ShowWindow(ClientCore.UIWindowType.UI_AFFICHE, true, null, affiche);
                }
            }
        }

        public UnityEngine.Object LoadResource(string resourceFile, Type type) {
            UnityEngine.Object resourceObject = null;

            string resourceProjectPath = Path.ChangeExtension(PathUtils.CombinePath(PathUtils.ResourcesProjectPath, resourceFile), PatchUtils.BundleExtensionName);
            if (m_BundleDict.ContainsKey(resourceProjectPath)) {
                resourceObject = m_BundleDict[resourceProjectPath];
            }
            if (null != resourceObject) {
                return resourceObject;
            }
            resourceObject = LoadResourceFromAssetBundle(resourceProjectPath);
            if (null != resourceObject) {
                m_BundleDict[resourceProjectPath] = resourceObject;
                return resourceObject;
            }
            return Resources.Load(resourceFile, type);
        }

        public UnityEngine.Object LoadResourceFromAssetBundle(string projectPath, bool unLoad = true) {
            string resourceArchivePath = PathUtils.GetArchivePathFromProjectPath(projectPath);

            string resourceName = PathUtils.GetPatchBundleKeyFromProjectPath(projectPath);
            //如果下载列表里面已有 则不用本地资源
            if (!BundlesManual.Contains(resourceName) || !File.Exists(resourceArchivePath)) {
                return null;
            }

            AssetBundle assetBundle = AssetBundle.CreateFromFile(resourceArchivePath);
            if (assetBundle == null) {
                GlobalToolsFunction.Log("ReadAssetBundle err! " + resourceArchivePath);
                return null;
            }
            return LoadMainAsset(assetBundle, unLoad);
        }

        private UnityEngine.Object LoadMainAsset(AssetBundle assetBundle, bool unLoad = false) {
            if (null == assetBundle) {
                return null;
            }
            UnityEngine.Object result = null;
            try {
                assetBundle.LoadAll();
                result = assetBundle.mainAsset;
            } catch (Exception e) {
                ClientCore.GlobalToolsFunction.LogException(e);
            }
            if (unLoad)
                assetBundle.Unload(false);

            return result;
        }

        public void LoadLevel(string levelName) {
            Operation operation = LoadLevelAsync(levelName);
            operation.Release();
        }
        public void LoadLevelAdditive(string levelName) {
            LoadLevelDependencies(levelName);
            Application.LoadLevelAdditive(levelName);
        }
        public Operation LoadLevelAsync(string levelName) {
            Operation operation = LoadLevelDependencies(levelName);
            operation.ao = Application.LoadLevelAsync(levelName);
            return operation;
        }
        public Operation LoadLevelAdditiveAsync(string levelName) {
            Operation operation = LoadLevelDependencies(levelName);
            operation.ao = Application.LoadLevelAdditiveAsync(levelName);
            return operation;
        }
        private Operation LoadLevelDependencies(string levelName) {
            Operation operation = new Operation();

            string text = Path.ChangeExtension(PathUtils.CombinePath(PathUtils.ScenesProjectPath, levelName), PatchUtils.BundleExtensionName);

            string path = PathUtils.GetFileFullPathFromArchivePath(text);
            if (!this.BundlesManual.Contains(text)) {
                return operation;
            }
            if (!File.Exists(path)) {
                GlobalToolsFunction.LogError("can not load level named " + levelName + " , failed to load the level dependencies");
                return operation;
            }
            operation.bundle = AssetBundle.CreateFromFile(path);
            //this.m_LevelPreloaders.Add(operation);
            return operation;
        }
        /// <summary>
        /// 保存更新包版本文件
        /// </summary>
        private void SavePatchVersion() {
            if (m_CurrentPatchVersion != null) {
                m_CurrentPatchVersion.WriteToFile(PathUtils.PatchVersionFile);
                m_CurrentPatchVersion = null;
            }
        }
        /// <summary>
        /// 下载更新包版本文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="actErr"></param>
        /// <returns></returns>
        private IEnumerator DownloadPatchVersion(string url, Action<string> actErr) {
            GlobalToolsFunction.Log("start DownloadPatch url " + url);
            string uri = System.Uri.EscapeUriString(url);
            GlobalToolsFunction.Log("start DownloadPatch uri " + uri);

            m_CurrentWWWDownload = new WWW(uri);
            yield return m_CurrentWWWDownload;
            if (!string.IsNullOrEmpty(m_CurrentWWWDownload.error)) {
                if (null != actErr) {
                    GlobalToolsFunction.LogError("download " + uri + " error:" + m_CurrentWWWDownload.error);
                    actErr(m_CurrentWWWDownload.error);
                }
                yield break;
            }
            if (m_CurrentWWWDownload.bytes.Length == 0) {
                GlobalToolsFunction.LogError("download error wdl.bytes.Length = 0");
            } else {
                GlobalToolsFunction.Log("download error wdl.bytes.Length = " + m_CurrentWWWDownload.bytes.Length);
            }
            m_TempPatchVersion = new PatchVersion(new MemoryStream(m_CurrentWWWDownload.bytes));

            m_CurrentWWWDownload = null;
            yield return null;
        }
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="fileDownloadUrl"></param>
        /// <param name="actErr"></param>
        /// <returns></returns>
        private IEnumerator DownloadFile(string fileDownloadUrl, Action<string> actErr) {
            string targetPath = PathUtils.CombinePath(PathUtils.ArchivePath, Path.GetFileName(fileDownloadUrl));
            GlobalToolsFunction.Log("start DownloadFile target " + targetPath);
            string fileDownloadUri = System.Uri.EscapeUriString(fileDownloadUrl);
            GlobalToolsFunction.Log("start DownloadFile " + fileDownloadUri);

            m_CurrentWWWDownload = new WWW(fileDownloadUri);
            yield return m_CurrentWWWDownload;
            if (!string.IsNullOrEmpty(m_CurrentWWWDownload.error)) {
                if (null != actErr) {
                    GlobalToolsFunction.LogError("download " + fileDownloadUrl + " error" + m_CurrentWWWDownload.error);
                    actErr(m_CurrentWWWDownload.error);
                }
                yield break;
            }

            GlobalToolsFunction.Log("start create " + PathUtils.ArchivePath);
            PathUtils.DirectoryCreate(PathUtils.ArchivePath);
            GlobalToolsFunction.Log("start delete " + targetPath);
            FileUtils.FileDelete(targetPath);
            if (File.Exists(targetPath)) {
                GlobalToolsFunction.LogError("DownloadFile does not delete old file " + targetPath);
            }

            FileUtils.WriteAllBytes(targetPath, m_CurrentWWWDownload.bytes);

            if (!File.Exists(targetPath)) {
                GlobalToolsFunction.LogError("DownloadFile fail " + targetPath + " from " + fileDownloadUri);
            }

            m_CurrentWWWDownload = null;
            yield return null;
        }
        /// <summary>
        /// 下载服务器列表
        /// </summary>
        /// <returns></returns>
        public IEnumerator DownloadServerList() {
            string err = string.Empty;
            System.Action<string> actErr = (string dlErr) => { err = dlErr; };

            yield return CoroutineBehaviour.StartCoroutine(DownloadFile(PathUtils.ServerListDownloadUrl, actErr));

            if (!string.IsNullOrEmpty(err)) {
                GlobalToolsFunction.LogError("Download serverlist fail!");
            }
            yield return new WaitForSeconds(WAIT_SECOND);
        }
        /// <summary>
        /// 下载告示
        /// </summary>
        /// <returns></returns>
        public IEnumerator DownloadAffiche() {
            string err = string.Empty;
            System.Action<string> actErr = (string dlErr) => { err = dlErr; };

            yield return CoroutineBehaviour.StartCoroutine(DownloadFile(PathUtils.AfficheDownloadUrl, actErr));

            if (!string.IsNullOrEmpty(err)) {
                GlobalToolsFunction.LogError("Download affiche fail!");
            }
            yield return new WaitForSeconds(WAIT_SECOND);
        }
        /// <summary>
        /// 同步检查更新
        /// </summary>
        /// <returns></returns>
        private IEnumerator AsynCheckPatch() {
            string err = string.Empty;
            System.Action<string> actErr = (string dlErr) => { err = dlErr; };

            yield return CoroutineBehaviour.StartCoroutine(
                DownloadPatchVersion(PathUtils.PatchVersionDownloadUrl, actErr));
            if (!string.IsNullOrEmpty(err)) {
                GlobalToolsFunction.Log("Download PatchVersion error! err=" + err.ToString());
                yield return new WaitForSeconds(WAIT_SECOND);
                m_CompletedCheckUpdate = true;
                yield break;
            }

            string clientVersion = Version.GetAppVersion();

            GlobalToolsFunction.Log("ArchivePath = " + PathUtils.ArchivePath);
            GlobalToolsFunction.Log("App Version = " + m_TempPatchVersion.AppVersion + " Client Version = " + clientVersion);
            GlobalToolsFunction.Log("Old Patch Version = " + m_CurrentPatchVersion.Version + " Patch Version = " + m_TempPatchVersion.Version);

            if (string.IsNullOrEmpty(m_TempPatchVersion.AppVersion)) {
                BundlesManual = new HashSet<string>();
                GlobalToolsFunction.LogError("CompareVerion error!");
                yield return new WaitForSeconds(WAIT_SECOND);
                m_CompletedCheckUpdate = true;
                yield break;
            } else if (!clientVersion.Equals(m_TempPatchVersion.AppVersion)) {
                BundlesManual = new HashSet<string>();
                yield return new WaitForSeconds(WAIT_SECOND);
                m_CompletedCheckUpdate = true;
                WMessageBox.Singleton.ShowWith(ClientCore.MESSAGEBOX_TYPE.E_UPDATE_VERSION);
                m_IsPauseUpdate = true;
                FileUtils.FileDelete(PathUtils.PatchVersionFile);
                yield break;
            }

            foreach (KeyValuePair<string, string> kvp in m_TempPatchVersion.PatchFile) {
                if (!m_CurrentPatchVersion.PatchFile.ContainsKey(kvp.Key)) {
                    m_TempPatchDownloadList.Add(kvp.Value);
                }
            }

            yield return new WaitForSeconds(WAIT_SECOND);
            m_CompletedCheckUpdate = true;
            GlobalToolsFunction.Log("CheckPatch completed!");
        }
        /// <summary>
        /// 同步下载更新
        /// </summary>
        /// <returns></returns>
        private IEnumerator AsynDownloadPatch() {
            string err = string.Empty;
            System.Action<string> actErr = (string dlErr) => { err = dlErr; };

            List<string> targetPatchsList = new List<string>();

            //download patch packages
            for (int i = 0; i < m_TempPatchDownloadList.Count; i++) {
                string patch = m_TempPatchDownloadList[i];

                ShowDownloadMessage(DownloadMsgCode.DOWNLOAD_PATCH_ING, patch);
                yield return null;

                yield return CoroutineBehaviour.StartCoroutine(
                    DownloadFile(PatchUtils.GetPatchDownloadURLFromPatchURI(patch), actErr));

                if (!string.IsNullOrEmpty(err)) {
                    GlobalToolsFunction.Log("Download patch file error! err=" + err.ToString());
                    ShowDownloadMessage(DownloadMsgCode.DOWNLOAD_PATCH_ERR);
                    yield return new WaitForSeconds(WAIT_SECOND);
                    break;
                }
                string fileName = PathUtils.GetPatchBundleKeyFromProjectPath(patch);

                targetPatchsList.Add(PathUtils.CombinePath(PathUtils.ArchivePath, fileName));
            }
            m_TempPatchDownloadList = null;

            GlobalToolsFunction.Log("Download target Patch Count = " + targetPatchsList.Count);

            string oldVersion = string.Empty;
            string newVersion = string.Empty;
            if (targetPatchsList.Count > 0) {
                yield return new WaitForSeconds(WAIT_SECOND);

                m_ChunkTotalCount = m_ChunkSetupedCount = 0;
                //calculate number all patch chunks
                foreach (string patch in targetPatchsList) {
                    m_ChunkTotalCount += CalculateChunkCount(patch);
                }
                yield return null;

                ShowDownloadMessage(DownloadMsgCode.UPDATE_RES_SIZE, String.Format("{0:0.##}", m_TempPatchVersion.SizeInKB / 1024.0f / 1024.0f));
                yield return new WaitForSeconds(WAIT_SECOND);

                //decompress all patch chunks data
                PakHeader pakHeader = new PakHeader();
                for (int i = 0; i < targetPatchsList.Count; i++) {
                    string patch = targetPatchsList[i];

                    yield return CoroutineBehaviour.StartCoroutine(SetupBundlePak(patch, pakHeader, actErr));

                    if (!string.IsNullOrEmpty(err)) {
                        GlobalToolsFunction.Log("SetupBundlePak error! err=" + err.ToString());
                        ShowDownloadMessage(DownloadMsgCode.PATCH_INS_ERR);
                        yield return new WaitForSeconds(WAIT_SECOND);
                        m_CompletedDownloadUpdate = true;
                        yield break;
                    }
                    PatchVersion.GetPatchVersionFromPatchFile(patch, ref oldVersion, ref newVersion);
                    m_CurrentPatchVersion.AddPatch(newVersion, Path.GetFileName(patch));
                }
            }
            m_CurrentPatchVersion.AddPatchSize(m_TempPatchVersion.SizeInKB);
            //本次更新成功，保存patchVersion文件
            SavePatchVersion();

            ShowDownloadMessage(DownloadMsgCode.UPDATE_FLINSH);

            if (m_IsNeedRestart) {
                //GameDevice.RestartApp();
                WMessageBox.Singleton.ShowWith(ClientCore.MESSAGEBOX_TYPE.E_HOT_UPDATE_RESTART, GlobalToolsFunction.OnRestartApp);
            }

            yield return new WaitForSeconds(WAIT_SECOND);
            m_CompletedDownloadUpdate = true;
            GlobalToolsFunction.Log("DownloadPatch completed!");
        }
        /// <summary>
        /// 计算进度
        /// </summary>
        /// <returns></returns>
        public float CalculateProgress() {
            float progress = Global.INVALID_FLOAT;
            if (null != m_CurrentWWWDownload) {
                progress = m_CurrentWWWDownload.progress;
            } else if (m_ChunkTotalCount > 0) {
                progress = m_ChunkSetupedCount * 1.0f / m_ChunkTotalCount;
            }
            return progress;
        }
        /// <summary>
        /// 计算总资源数
        /// </summary>
        /// <param name="patchFile"></param>
        /// <returns></returns>
        private int CalculateChunkCount(string patchFile) {
            if (!File.Exists(patchFile)) {
                return 0;
            }
            int result = 0;
            FileStream fileStream = new FileStream(patchFile, FileMode.Open);
            try {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Deserialize(fileStream);
                binaryFormatter.Deserialize(fileStream);
                result = Convert.ToInt32(binaryFormatter.Deserialize(fileStream));
            } catch (Exception ex) {
                GlobalToolsFunction.LogError(ex.Message);
                return 0;
            } finally {
                fileStream.Close();
            }
            return result;
        }
        /// <summary>
        /// 安装bundle
        /// </summary>
        /// <param name="pakFile"></param>
        /// <param name="pakHeader"></param>
        /// <param name="actError"></param>
        /// <returns></returns>
        private IEnumerator SetupBundlePak(string pakFile, PakHeader pakHeader, Action<string> actError) {
            if (!File.Exists(pakFile)) {
                if (null != actError) {
                    actError("");
                }
                yield break;
            }

            FileStream stream = new FileStream(pakFile, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();

            try {
                pakHeader.m_Version = Convert.ToString(formatter.Deserialize(stream));
                pakHeader.m_Platform = Convert.ToString(formatter.Deserialize(stream));
                pakHeader.m_Chunk = Convert.ToInt32(formatter.Deserialize(stream));

                GlobalToolsFunction.Log("SetupBundlePak header Version=" + pakHeader.m_Version + " Platform=" + pakHeader.m_Platform);
            } catch (Exception e) {
                if (null != actError)
                    actError(e.Message);

                stream.Close();

                FileUtils.FileDelete(pakFile);

                yield break;
            }

            HashSet<string> manual = m_BundlesManual;
            for (int i = 0; i != pakHeader.m_Chunk; ++i) {
                string bundleFilePath = string.Empty;
                BundleChunk chunk = null;
                try {
                    chunk = formatter.Deserialize(stream) as BundleChunk;
                    bundleFilePath = PathUtils.GetFileFullPathFromArchivePath(PathUtils.GetProjectPathFromPatchBundleKey(chunk.m_Key));
                    bundleFilePath = PatchUtils.GetLibraryNameFromProjectPath(bundleFilePath);
                } catch (System.Exception e) {
                    if (null != actError) {
                        actError(e.Message);
                    }
                    stream.Close();
                    FileUtils.FileDelete(pakFile);
                    yield break;
                }

                ShowDownloadMessage(DownloadMsgCode.UPDATE_RES_ING, Path.GetFileName(bundleFilePath));
                yield return null;

                try {
                    PathUtils.DirectoryCreate(Path.GetDirectoryName(bundleFilePath));
                    FileUtils.FileDelete(bundleFilePath);
                    FileUtils.WriteAllBytes(bundleFilePath, chunk.m_Data);

                    SevenZip.SevenZipCompressor.DecompressAssetBundle(bundleFilePath, bundleFilePath);

                    GlobalToolsFunction.LogInfo("SetupBundlePak DecompressAssetBundle = " + bundleFilePath);

                    string md5 = FileUtils.GenMd5Str(bundleFilePath);

                    if (md5 != chunk.m_Md5) {
                        GlobalToolsFunction.LogError("failed to verify bundle chunk named " + chunk.m_Key);
                    }

                    manual.Add(chunk.m_Key);

                    if (!m_IsPauseUpdate && ClientApp.IsNeedReInit(Path.GetFileName(bundleFilePath))) {
                        m_IsPauseUpdate = true;
                        m_IsNeedRestart = true;
                    }
                } catch (System.Exception e) {
                    if (null != actError)
                        actError(e.Message);

                    stream.Close();

                    FileUtils.FileDelete(pakFile);

                    yield break;
                }

                ++m_ChunkSetupedCount;
            }

            BundlesManual = manual;
            stream.Close();

            FileUtils.FileDelete(pakFile);
            yield return null;
        }
        /// <summary>
        /// 点击热更新
        /// </summary>
        /// <param name="buttonType"></param>
        /// <param name="param"></param>
        private void _OnClickHotupdate(MSG_BOTTON_TYPE buttonType, params object[] param) {
            if (buttonType == MSG_BOTTON_TYPE.CANCEL || buttonType == MSG_BOTTON_TYPE.CLOSE) {
                GameDevice.QuitApp();
            } else if (buttonType == MSG_BOTTON_TYPE.CONFIRM) {
                CoroutineBehaviour.StartCoroutine(AsynDownloadPatch());
                GlobalToolsFunction.Log("DownloadPatch completed!");
            }
        }
    } */
}