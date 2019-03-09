//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.7.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from LuaCompletion2.g4 by ANTLR 4.7.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="LuaCompletion2Parser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.7.1")]
[System.CLSCompliant(false)]
public interface ILuaCompletion2Listener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="LuaCompletion2Parser.chunk"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterChunk([NotNull] LuaCompletion2Parser.ChunkContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LuaCompletion2Parser.chunk"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitChunk([NotNull] LuaCompletion2Parser.ChunkContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LuaCompletion2Parser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBlock([NotNull] LuaCompletion2Parser.BlockContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LuaCompletion2Parser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBlock([NotNull] LuaCompletion2Parser.BlockContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LuaCompletion2Parser.stat"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStat([NotNull] LuaCompletion2Parser.StatContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LuaCompletion2Parser.stat"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStat([NotNull] LuaCompletion2Parser.StatContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LuaCompletion2Parser.retstat"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterRetstat([NotNull] LuaCompletion2Parser.RetstatContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LuaCompletion2Parser.retstat"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitRetstat([NotNull] LuaCompletion2Parser.RetstatContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LuaCompletion2Parser.label"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLabel([NotNull] LuaCompletion2Parser.LabelContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LuaCompletion2Parser.label"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLabel([NotNull] LuaCompletion2Parser.LabelContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LuaCompletion2Parser.funcname"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFuncname([NotNull] LuaCompletion2Parser.FuncnameContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LuaCompletion2Parser.funcname"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFuncname([NotNull] LuaCompletion2Parser.FuncnameContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LuaCompletion2Parser.varlist"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterVarlist([NotNull] LuaCompletion2Parser.VarlistContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LuaCompletion2Parser.varlist"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitVarlist([NotNull] LuaCompletion2Parser.VarlistContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LuaCompletion2Parser.namelist"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNamelist([NotNull] LuaCompletion2Parser.NamelistContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LuaCompletion2Parser.namelist"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNamelist([NotNull] LuaCompletion2Parser.NamelistContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LuaCompletion2Parser.explist"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExplist([NotNull] LuaCompletion2Parser.ExplistContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LuaCompletion2Parser.explist"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExplist([NotNull] LuaCompletion2Parser.ExplistContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LuaCompletion2Parser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExp([NotNull] LuaCompletion2Parser.ExpContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LuaCompletion2Parser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExp([NotNull] LuaCompletion2Parser.ExpContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LuaCompletion2Parser.prefixexp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPrefixexp([NotNull] LuaCompletion2Parser.PrefixexpContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LuaCompletion2Parser.prefixexp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPrefixexp([NotNull] LuaCompletion2Parser.PrefixexpContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LuaCompletion2Parser.functioncall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFunctioncall([NotNull] LuaCompletion2Parser.FunctioncallContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LuaCompletion2Parser.functioncall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFunctioncall([NotNull] LuaCompletion2Parser.FunctioncallContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LuaCompletion2Parser.varOrExp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterVarOrExp([NotNull] LuaCompletion2Parser.VarOrExpContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LuaCompletion2Parser.varOrExp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitVarOrExp([NotNull] LuaCompletion2Parser.VarOrExpContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LuaCompletion2Parser.var"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterVar([NotNull] LuaCompletion2Parser.VarContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LuaCompletion2Parser.var"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitVar([NotNull] LuaCompletion2Parser.VarContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LuaCompletion2Parser.varSuffix"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterVarSuffix([NotNull] LuaCompletion2Parser.VarSuffixContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LuaCompletion2Parser.varSuffix"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitVarSuffix([NotNull] LuaCompletion2Parser.VarSuffixContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LuaCompletion2Parser.terminalVar"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterTerminalVar([NotNull] LuaCompletion2Parser.TerminalVarContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LuaCompletion2Parser.terminalVar"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitTerminalVar([NotNull] LuaCompletion2Parser.TerminalVarContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LuaCompletion2Parser.terminalVarSuffix"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterTerminalVarSuffix([NotNull] LuaCompletion2Parser.TerminalVarSuffixContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LuaCompletion2Parser.terminalVarSuffix"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitTerminalVarSuffix([NotNull] LuaCompletion2Parser.TerminalVarSuffixContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LuaCompletion2Parser.nameAndArgs"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNameAndArgs([NotNull] LuaCompletion2Parser.NameAndArgsContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LuaCompletion2Parser.nameAndArgs"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNameAndArgs([NotNull] LuaCompletion2Parser.NameAndArgsContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LuaCompletion2Parser.newExp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNewExp([NotNull] LuaCompletion2Parser.NewExpContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LuaCompletion2Parser.newExp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNewExp([NotNull] LuaCompletion2Parser.NewExpContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LuaCompletion2Parser.arrayAccessExp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterArrayAccessExp([NotNull] LuaCompletion2Parser.ArrayAccessExpContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LuaCompletion2Parser.arrayAccessExp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitArrayAccessExp([NotNull] LuaCompletion2Parser.ArrayAccessExpContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LuaCompletion2Parser.args"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterArgs([NotNull] LuaCompletion2Parser.ArgsContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LuaCompletion2Parser.args"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitArgs([NotNull] LuaCompletion2Parser.ArgsContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LuaCompletion2Parser.functiondef"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFunctiondef([NotNull] LuaCompletion2Parser.FunctiondefContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LuaCompletion2Parser.functiondef"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFunctiondef([NotNull] LuaCompletion2Parser.FunctiondefContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LuaCompletion2Parser.funcbody"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFuncbody([NotNull] LuaCompletion2Parser.FuncbodyContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LuaCompletion2Parser.funcbody"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFuncbody([NotNull] LuaCompletion2Parser.FuncbodyContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LuaCompletion2Parser.parlist"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterParlist([NotNull] LuaCompletion2Parser.ParlistContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LuaCompletion2Parser.parlist"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitParlist([NotNull] LuaCompletion2Parser.ParlistContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LuaCompletion2Parser.tableconstructor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterTableconstructor([NotNull] LuaCompletion2Parser.TableconstructorContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LuaCompletion2Parser.tableconstructor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitTableconstructor([NotNull] LuaCompletion2Parser.TableconstructorContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LuaCompletion2Parser.fieldlist"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFieldlist([NotNull] LuaCompletion2Parser.FieldlistContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LuaCompletion2Parser.fieldlist"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFieldlist([NotNull] LuaCompletion2Parser.FieldlistContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LuaCompletion2Parser.field"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterField([NotNull] LuaCompletion2Parser.FieldContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LuaCompletion2Parser.field"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitField([NotNull] LuaCompletion2Parser.FieldContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LuaCompletion2Parser.terminalField"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterTerminalField([NotNull] LuaCompletion2Parser.TerminalFieldContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LuaCompletion2Parser.terminalField"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitTerminalField([NotNull] LuaCompletion2Parser.TerminalFieldContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LuaCompletion2Parser.fieldsep"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFieldsep([NotNull] LuaCompletion2Parser.FieldsepContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LuaCompletion2Parser.fieldsep"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFieldsep([NotNull] LuaCompletion2Parser.FieldsepContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LuaCompletion2Parser.operatorOr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterOperatorOr([NotNull] LuaCompletion2Parser.OperatorOrContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LuaCompletion2Parser.operatorOr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitOperatorOr([NotNull] LuaCompletion2Parser.OperatorOrContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LuaCompletion2Parser.operatorAnd"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterOperatorAnd([NotNull] LuaCompletion2Parser.OperatorAndContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LuaCompletion2Parser.operatorAnd"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitOperatorAnd([NotNull] LuaCompletion2Parser.OperatorAndContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LuaCompletion2Parser.operatorComparison"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterOperatorComparison([NotNull] LuaCompletion2Parser.OperatorComparisonContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LuaCompletion2Parser.operatorComparison"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitOperatorComparison([NotNull] LuaCompletion2Parser.OperatorComparisonContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LuaCompletion2Parser.operatorStrcat"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterOperatorStrcat([NotNull] LuaCompletion2Parser.OperatorStrcatContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LuaCompletion2Parser.operatorStrcat"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitOperatorStrcat([NotNull] LuaCompletion2Parser.OperatorStrcatContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LuaCompletion2Parser.operatorAddSub"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterOperatorAddSub([NotNull] LuaCompletion2Parser.OperatorAddSubContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LuaCompletion2Parser.operatorAddSub"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitOperatorAddSub([NotNull] LuaCompletion2Parser.OperatorAddSubContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LuaCompletion2Parser.operatorMulDivMod"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterOperatorMulDivMod([NotNull] LuaCompletion2Parser.OperatorMulDivModContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LuaCompletion2Parser.operatorMulDivMod"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitOperatorMulDivMod([NotNull] LuaCompletion2Parser.OperatorMulDivModContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LuaCompletion2Parser.operatorBitwise"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterOperatorBitwise([NotNull] LuaCompletion2Parser.OperatorBitwiseContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LuaCompletion2Parser.operatorBitwise"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitOperatorBitwise([NotNull] LuaCompletion2Parser.OperatorBitwiseContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LuaCompletion2Parser.operatorUnary"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterOperatorUnary([NotNull] LuaCompletion2Parser.OperatorUnaryContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LuaCompletion2Parser.operatorUnary"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitOperatorUnary([NotNull] LuaCompletion2Parser.OperatorUnaryContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LuaCompletion2Parser.operatorPower"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterOperatorPower([NotNull] LuaCompletion2Parser.OperatorPowerContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LuaCompletion2Parser.operatorPower"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitOperatorPower([NotNull] LuaCompletion2Parser.OperatorPowerContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LuaCompletion2Parser.number"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNumber([NotNull] LuaCompletion2Parser.NumberContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LuaCompletion2Parser.number"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNumber([NotNull] LuaCompletion2Parser.NumberContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="LuaCompletion2Parser.string"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterString([NotNull] LuaCompletion2Parser.StringContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LuaCompletion2Parser.string"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitString([NotNull] LuaCompletion2Parser.StringContext context);
}