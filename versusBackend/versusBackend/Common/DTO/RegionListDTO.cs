//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: RegionListDTO.proto
// Note: requires additional types generated from: RegionDTO.proto
namespace Common.DTO
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RegionListDTO")]
  public partial class RegionListDTO : global::ProtoBuf.IExtensible
  {
    public RegionListDTO() {}
    
    private readonly global::System.Collections.Generic.List<Common.DTO.RegionDTO> _RegionList = new global::System.Collections.Generic.List<Common.DTO.RegionDTO>();
    [global::ProtoBuf.ProtoMember(1, Name=@"RegionList", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Common.DTO.RegionDTO> RegionList
    {
      get { return _RegionList; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}