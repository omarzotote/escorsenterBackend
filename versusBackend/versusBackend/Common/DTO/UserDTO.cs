//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: UserDTO.proto
namespace Common.DTO
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"UserDTO")]
  public partial class UserDTO : global::ProtoBuf.IExtensible
  {
    public UserDTO() {}
    
    private string _UserName = "";
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"UserName", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string UserName
    {
      get { return _UserName; }
      set { _UserName = value; }
    }
    private string _Password = "";
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"Password", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string Password
    {
      get { return _Password; }
      set { _Password = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}