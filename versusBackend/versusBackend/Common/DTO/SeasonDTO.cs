//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: SeasonDTO.proto
// Note: requires additional types generated from: ScoreTableResultDTO.proto
// Note: requires additional types generated from: WeekDTO.proto
namespace Common.DTO
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"SeasonDTO")]
  public partial class SeasonDTO : global::ProtoBuf.IExtensible
  {
    public SeasonDTO() {}
    
    private long _Id = default(long);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"Id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(long))]
    public long Id
    {
      get { return _Id; }
      set { _Id = value; }
    }
    private long _League = default(long);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"League", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(long))]
    public long League
    {
      get { return _League; }
      set { _League = value; }
    }
    private string _Title = "";
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"Title", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string Title
    {
      get { return _Title; }
      set { _Title = value; }
    }
    private readonly global::System.Collections.Generic.List<Common.DTO.ScoreTableResultDTO> _ScoreTableResult = new global::System.Collections.Generic.List<Common.DTO.ScoreTableResultDTO>();
    [global::ProtoBuf.ProtoMember(4, Name=@"ScoreTableResult", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Common.DTO.ScoreTableResultDTO> ScoreTableResult
    {
      get { return _ScoreTableResult; }
    }
  
    private readonly global::System.Collections.Generic.List<Common.DTO.WeekDTO> _Weeks = new global::System.Collections.Generic.List<Common.DTO.WeekDTO>();
    [global::ProtoBuf.ProtoMember(5, Name=@"Weeks", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Common.DTO.WeekDTO> Weeks
    {
      get { return _Weeks; }
    }
  
    private string _Description = "";
    [global::ProtoBuf.ProtoMember(16, IsRequired = false, Name=@"Description", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string Description
    {
      get { return _Description; }
      set { _Description = value; }
    }
    private string _DateFrom = "";
    [global::ProtoBuf.ProtoMember(17, IsRequired = false, Name=@"DateFrom", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string DateFrom
    {
      get { return _DateFrom; }
      set { _DateFrom = value; }
    }
    private string _DateTo = "";
    [global::ProtoBuf.ProtoMember(18, IsRequired = false, Name=@"DateTo", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string DateTo
    {
      get { return _DateTo; }
      set { _DateTo = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}