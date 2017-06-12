using UnityEngine;
using System.Text;
using System.Collections.Generic;

[RequireComponent(typeof(UILabel))]
public class TypeWriter : MonoBehaviour
{
    static public TypeWriter current;

    // 文字显示速度
    public float charsPerSecond = 30f;

    // 打字结束后委托
    public List<EventDelegate> onFinished = new List<EventDelegate>();

    UILabel mLabel;

    // 全文本
    string mFullText = "";
    int mCurrentOffset = 0;
    float mNextChar = 0f;
    bool mReset = true;
    bool mFinished = false;

    /// <summary>
    /// 判断是否结束打字
    /// </summary>
    public bool isFinish { get { return mFinished; } }

    /// <summary>
    /// 重新开始打字效果
    /// </summary>
    public void ResetToBeginning()
    {
        mFinished = false;
        mReset = true;
        mNextChar = 0f;
        mCurrentOffset = 0;
        Update();
    }

    /// <summary>
    /// 结束打字 显示全部文本
    /// </summary>
    public void Finish()
    {
        if (!mFinished)
        {
            mFinished = true;
            mCurrentOffset = mFullText.Length;
            mLabel.text = mFullText;
            current = this;
            EventDelegate.Execute(onFinished);
            current = null;
        }
    }

    //void OnEnable() { mReset = true; mFinished = false; }

    void OnDisable() { Finish(); }

    void Update()
    {
        if (mFinished) return;
        if (mReset)
        {
            mCurrentOffset = 0;
            mReset = false;
            mLabel = GetComponent<UILabel>();
            mFullText = mLabel.processedText;
        }
        if (string.IsNullOrEmpty(mFullText)) return;

        while (mCurrentOffset < mFullText.Length && mNextChar <= RealTime.time)
        {
            int lastOffset = mCurrentOffset;
            charsPerSecond = Mathf.Max(1, charsPerSecond);

            // Automatically skip all symbols
            if (mLabel.supportEncoding)
                while (NGUIText.ParseSymbol(mFullText, ref mCurrentOffset)) { }

            ++mCurrentOffset;

            // Reached the end? We're done.
            if (mCurrentOffset > mFullText.Length) break;

            float delay = 1f / charsPerSecond;

            if (mNextChar == 0f)
            {
                mNextChar = RealTime.time + delay;
            }
            else
            {
                mNextChar += delay;
            }
            mLabel.text = mFullText.Substring(0, mCurrentOffset);
        }

        // Alpha-based fading
        if (mFullText.Length > 0 && mCurrentOffset >= mFullText.Length)
        {
            mLabel.text = mFullText;
            current = this;
            EventDelegate.Execute(onFinished);
            current = null;
            mFinished = true;
        }

    }

}
