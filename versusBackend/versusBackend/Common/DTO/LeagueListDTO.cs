//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: LeagueListDTO.proto
// Note: requires additional types generated from: LeagueDTO.proto
namespace Common.DTO
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LeagueListDTO")]
  public partial class LeagueListDTO : global::ProtoBuf.IExtensible
  {
    public LeagueListDTO() {}
    
    private readonly global::System.Collections.Generic.List<Common.DTO.LeagueDTO> _items = new global::System.Collections.Generic.List<Common.DTO.LeagueDTO>();
    [global::ProtoBuf.ProtoMember(1, Name=@"items", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Common.DTO.LeagueDTO> items
    {
      get { return _items; }
    }
  
    private int _PageNumber = default(int);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"PageNumber", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int PageNumber
    {
      get { return _PageNumber; }
      set { _PageNumber = value; }
    }
    private int _PageTotal = default(int);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"PageTotal", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int PageTotal
    {
      get { return _PageTotal; }
      set { _PageTotal = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}