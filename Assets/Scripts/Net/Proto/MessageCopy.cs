//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: MessageCopy.proto
// Note: requires additional types generated from: MessageID.proto
// Note: requires additional types generated from: MessageRetCode.proto
// Note: requires additional types generated from: MessageCommon.proto
namespace Protocol
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ReqBattleCheck")]
  public partial class ReqBattleCheck : global::ProtoBuf.IExtensible
  {
    public ReqBattleCheck() {}
    
    private int _CopyType;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"CopyType", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int CopyType
    {
      get { return _CopyType; }
      set { _CopyType = value; }
    }
    private int _Chapter;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"Chapter", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int Chapter
    {
      get { return _Chapter; }
      set { _Chapter = value; }
    }
    private int _CopyID;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"CopyID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int CopyID
    {
      get { return _CopyID; }
      set { _CopyID = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"AckBattleCheck")]
  public partial class AckBattleCheck : global::ProtoBuf.IExtensible
  {
    public AckBattleCheck() {}
    
    private int _CopyType;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"CopyType", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int CopyType
    {
      get { return _CopyType; }
      set { _CopyType = value; }
    }
    private int _Chapter;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"Chapter", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int Chapter
    {
      get { return _Chapter; }
      set { _Chapter = value; }
    }
    private int _CopyID;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"CopyID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int CopyID
    {
      get { return _CopyID; }
      set { _CopyID = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ReqPassCopy")]
  public partial class ReqPassCopy : global::ProtoBuf.IExtensible
  {
    public ReqPassCopy() {}
    
    private int _CopyType;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"CopyType", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int CopyType
    {
      get { return _CopyType; }
      set { _CopyType = value; }
    }
    private int _Chapter;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"Chapter", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int Chapter
    {
      get { return _Chapter; }
      set { _Chapter = value; }
    }
    private int _CopyID;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"CopyID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int CopyID
    {
      get { return _CopyID; }
      set { _CopyID = value; }
    }
    private int _StarNum;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"StarNum", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int StarNum
    {
      get { return _StarNum; }
      set { _StarNum = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"AckPassCopy")]
  public partial class AckPassCopy : global::ProtoBuf.IExtensible
  {
    public AckPassCopy() {}
    
    private int _CopyType;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"CopyType", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int CopyType
    {
      get { return _CopyType; }
      set { _CopyType = value; }
    }
    private int _Chapter;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"Chapter", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int Chapter
    {
      get { return _Chapter; }
      set { _Chapter = value; }
    }
    private int _CopyID;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"CopyID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int CopyID
    {
      get { return _CopyID; }
      set { _CopyID = value; }
    }
    private int _StarNum;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"StarNum", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int StarNum
    {
      get { return _StarNum; }
      set { _StarNum = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ReqGetChapterAward")]
  public partial class ReqGetChapterAward : global::ProtoBuf.IExtensible
  {
    public ReqGetChapterAward() {}
    
    private int _CopyType;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"CopyType", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int CopyType
    {
      get { return _CopyType; }
      set { _CopyType = value; }
    }
    private int _Chapter;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"Chapter", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int Chapter
    {
      get { return _Chapter; }
      set { _Chapter = value; }
    }
    private int _Index;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"Index", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int Index
    {
      get { return _Index; }
      set { _Index = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"AckGetChapterAward")]
  public partial class AckGetChapterAward : global::ProtoBuf.IExtensible
  {
    public AckGetChapterAward() {}
    
    private int _CopyType;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"CopyType", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int CopyType
    {
      get { return _CopyType; }
      set { _CopyType = value; }
    }
    private int _Chapter;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"Chapter", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int Chapter
    {
      get { return _Chapter; }
      set { _Chapter = value; }
    }
    private int _Index;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"Index", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int Index
    {
      get { return _Index; }
      set { _Index = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}