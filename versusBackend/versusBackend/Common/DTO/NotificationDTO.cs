//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: NotificationDTO.proto
namespace Common.DTO
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"NotificationDTO")]
  public partial class NotificationDTO : global::ProtoBuf.IExtensible
  {
    public NotificationDTO() {}
    
    private long _Id = default(long);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"Id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(long))]
    public long Id
    {
      get { return _Id; }
      set { _Id = value; }
    }
    private string _Title = "";
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"Title", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string Title
    {
      get { return _Title; }
      set { _Title = value; }
    }
    private string _Content = "";
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"Content", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string Content
    {
      get { return _Content; }
      set { _Content = value; }
    }
    private string _Date = "";
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"Date", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string Date
    {
      get { return _Date; }
      set { _Date = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}