namespace KARA
{
    public interface INativePlugin
    {
        /// <summary>
        /// 초기화 함수 반드시 초기화 해주어야 된다.
        /// </summary>
        /// <param name="unityObjName"></param>
        void Init(string unityObjName);

        /// <summary>
        /// 클립보드 복사 기능
        /// </summary>
        /// <param name="msg"> 복사할 메시지 </param>
        /// <returns> 복사 성공 여부</returns>
        bool ClipBoardCopy(string msg);
    }
}