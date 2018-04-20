using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 话题列表的详细信息
/// </summary>
public class TopicHintLabel : UIButtonMessage
{
    private NegotiateUIManager uiManager;
    private string topicName;
    private bool _currentOverState = false;

    public void SetUIManager(NegotiateUIManager uiManager)
    {
        this.uiManager = uiManager;
    }

    public void SetTopic(string xxx)
    {
        this.topicName = xxx;
    }

    private void OnPress(bool isDown)
    {
        _currentOverState = !isDown;
    }

    private void OnHover(bool isOver)
    {
        if (_currentOverState == isOver)
            return;
        _currentOverState = isOver;
        if (isOver)
        {
            //Debug.Log("in " + topicName);
            uiManager.ShowTopicHint(topicName, (int)transform.localPosition.y);
        }
        else
        {
            //Debug.Log("out " + topicName);
            uiManager.HideTopicHint(topicName);
        }
    }

}
