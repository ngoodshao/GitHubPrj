using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// MyTreeNode 的摘要说明
/// </summary>
public class MyTreeNode
{
    public MyTreeNode() { }
    public MyTreeNode(int _id, int _pid, string _text, string _url, bool _isLeaf, bool _isexpand, int _delay, string _iconClsFieldName)
    {
        this.id = _id;
        this.pid = _pid;
        this.text = _text;
        this.url = _url;
        this.isLeaf = _isLeaf;
        this.isexpand = _isexpand;
        this.delay = _delay;
        this.iconClsFieldName = _iconClsFieldName;
    }
    /// <summary>
    /// 节点ID
    /// </summary>
    public object id { set; get; }
    /// <summary>
    /// 父节点ID
    /// </summary>
    public object pid { set; get; }
    /// <summary>
    /// 显示文本
    /// </summary>
    public object text { set; get; }
    /// <summary>
    /// 节点Url
    /// </summary>
    public object url { set; get; }
    /// <summary>
    /// 是否有子节点
    /// </summary>
    public bool isLeaf { set; get; }
    /// <summary>
    /// 节点是否展开
    /// </summary>
    public bool isexpand { set; get; }
    /// <summary>
    /// 展开到第几层
    /// </summary>
    public int delay { set; get; }

    /// <summary>
    /// 附加图标的class后缀
    /// </summary>
    public string iconClsFieldName { set; get; }
}