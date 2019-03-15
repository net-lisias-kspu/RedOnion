// Generated from /home/developer/Sync/BigFiles/BigProjects/Kerbalua/Kerbalua/Kerbalua/Kerbalua/Completion/Grammar/IncompleteLua.g4 by ANTLR 4.7.1
import org.antlr.v4.runtime.Lexer;
import org.antlr.v4.runtime.CharStream;
import org.antlr.v4.runtime.Token;
import org.antlr.v4.runtime.TokenStream;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.misc.*;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class LuaLexer extends Lexer {
	static { RuntimeMetaData.checkVersion("4.7.1", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, T__8=9, 
		T__9=10, T__10=11, T__11=12, T__12=13, T__13=14, T__14=15, T__15=16, T__16=17, 
		T__17=18, T__18=19, T__19=20, T__20=21, T__21=22, T__22=23, T__23=24, 
		T__24=25, T__25=26, T__26=27, T__27=28, T__28=29, T__29=30, T__30=31, 
		T__31=32, T__32=33, T__33=34, T__34=35, T__35=36, T__36=37, T__37=38, 
		T__38=39, T__39=40, T__40=41, T__41=42, T__42=43, T__43=44, T__44=45, 
		T__45=46, T__46=47, T__47=48, T__48=49, T__49=50, T__50=51, T__51=52, 
		T__52=53, T__53=54, T__54=55, NAME=56, NORMALSTRING=57, CHARSTRING=58, 
		LONGSTRING=59, INT=60, HEX=61, FLOAT=62, HEX_FLOAT=63, COMMENT=64, LINE_COMMENT=65, 
		SHEBANG=66;
	public static String[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static String[] modeNames = {
		"DEFAULT_MODE"
	};

	public static final String[] ruleNames = {
		"T__0", "T__1", "T__2", "T__3", "T__4", "T__5", "T__6", "T__7", "T__8", 
		"T__9", "T__10", "T__11", "T__12", "T__13", "T__14", "T__15", "T__16", 
		"T__17", "T__18", "T__19", "T__20", "T__21", "T__22", "T__23", "T__24", 
		"T__25", "T__26", "T__27", "T__28", "T__29", "T__30", "T__31", "T__32", 
		"T__33", "T__34", "T__35", "T__36", "T__37", "T__38", "T__39", "T__40", 
		"T__41", "T__42", "T__43", "T__44", "T__45", "T__46", "T__47", "T__48", 
		"T__49", "T__50", "T__51", "T__52", "T__53", "T__54", "NAME", "NORMALSTRING", 
		"CHARSTRING", "LONGSTRING", "NESTED_STR", "INT", "HEX", "FLOAT", "HEX_FLOAT", 
		"ExponentPart", "HexExponentPart", "EscapeSequence", "DecimalEscape", 
		"HexEscape", "UtfEscape", "Digit", "HexDigit", "COMMENT", "LINE_COMMENT", 
		"SHEBANG"
	};

	private static final String[] _LITERAL_NAMES = {
		null, "';'", "'='", "'break'", "'goto'", "'do'", "'end'", "'while'", "'repeat'", 
		"'until'", "'if'", "'then'", "'elseif'", "'else'", "'for'", "','", "'in'", 
		"'function'", "'local'", "'return'", "'::'", "'.'", "':'", "'nil'", "'false'", 
		"'true'", "'...'", "'('", "')'", "'['", "']'", "'{'", "'}'", "'or'", "'and'", 
		"'<'", "'>'", "'<='", "'>='", "'~='", "'=='", "'..'", "'+'", "'-'", "'*'", 
		"'/'", "'%'", "'//'", "'&'", "'|'", "'~'", "'<<'", "'>>'", "'not'", "'#'", 
		"'^'"
	};
	private static final String[] _SYMBOLIC_NAMES = {
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, null, null, null, null, "NAME", "NORMALSTRING", 
		"CHARSTRING", "LONGSTRING", "INT", "HEX", "FLOAT", "HEX_FLOAT", "COMMENT", 
		"LINE_COMMENT", "SHEBANG"
	};
	public static final Vocabulary VOCABULARY = new VocabularyImpl(_LITERAL_NAMES, _SYMBOLIC_NAMES);

	/**
	 * @deprecated Use {@link #VOCABULARY} instead.
	 */
	@Deprecated
	public static final String[] tokenNames;
	static {
		tokenNames = new String[_SYMBOLIC_NAMES.length];
		for (int i = 0; i < tokenNames.length; i++) {
			tokenNames[i] = VOCABULARY.getLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = VOCABULARY.getSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}
	}

	@Override
	@Deprecated
	public String[] getTokenNames() {
		return tokenNames;
	}

	@Override

	public Vocabulary getVocabulary() {
		return VOCABULARY;
	}


	public LuaLexer(CharStream input) {
		super(input);
		_interp = new LexerATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	@Override
	public String getGrammarFileName() { return "IncompleteLua.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public String[] getChannelNames() { return channelNames; }

	@Override
	public String[] getModeNames() { return modeNames; }

	@Override
	public ATN getATN() { return _ATN; }

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\2D\u0252\b\1\4\2\t"+
		"\2\4\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13"+
		"\t\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22\t\22"+
		"\4\23\t\23\4\24\t\24\4\25\t\25\4\26\t\26\4\27\t\27\4\30\t\30\4\31\t\31"+
		"\4\32\t\32\4\33\t\33\4\34\t\34\4\35\t\35\4\36\t\36\4\37\t\37\4 \t \4!"+
		"\t!\4\"\t\"\4#\t#\4$\t$\4%\t%\4&\t&\4\'\t\'\4(\t(\4)\t)\4*\t*\4+\t+\4"+
		",\t,\4-\t-\4.\t.\4/\t/\4\60\t\60\4\61\t\61\4\62\t\62\4\63\t\63\4\64\t"+
		"\64\4\65\t\65\4\66\t\66\4\67\t\67\48\t8\49\t9\4:\t:\4;\t;\4<\t<\4=\t="+
		"\4>\t>\4?\t?\4@\t@\4A\tA\4B\tB\4C\tC\4D\tD\4E\tE\4F\tF\4G\tG\4H\tH\4I"+
		"\tI\4J\tJ\4K\tK\4L\tL\3\2\3\2\3\3\3\3\3\4\3\4\3\4\3\4\3\4\3\4\3\5\3\5"+
		"\3\5\3\5\3\5\3\6\3\6\3\6\3\7\3\7\3\7\3\7\3\b\3\b\3\b\3\b\3\b\3\b\3\t\3"+
		"\t\3\t\3\t\3\t\3\t\3\t\3\n\3\n\3\n\3\n\3\n\3\n\3\13\3\13\3\13\3\f\3\f"+
		"\3\f\3\f\3\f\3\r\3\r\3\r\3\r\3\r\3\r\3\r\3\16\3\16\3\16\3\16\3\16\3\17"+
		"\3\17\3\17\3\17\3\20\3\20\3\21\3\21\3\21\3\22\3\22\3\22\3\22\3\22\3\22"+
		"\3\22\3\22\3\22\3\23\3\23\3\23\3\23\3\23\3\23\3\24\3\24\3\24\3\24\3\24"+
		"\3\24\3\24\3\25\3\25\3\25\3\26\3\26\3\27\3\27\3\30\3\30\3\30\3\30\3\31"+
		"\3\31\3\31\3\31\3\31\3\31\3\32\3\32\3\32\3\32\3\32\3\33\3\33\3\33\3\33"+
		"\3\34\3\34\3\35\3\35\3\36\3\36\3\37\3\37\3 \3 \3!\3!\3\"\3\"\3\"\3#\3"+
		"#\3#\3#\3$\3$\3%\3%\3&\3&\3&\3\'\3\'\3\'\3(\3(\3(\3)\3)\3)\3*\3*\3*\3"+
		"+\3+\3,\3,\3-\3-\3.\3.\3/\3/\3\60\3\60\3\60\3\61\3\61\3\62\3\62\3\63\3"+
		"\63\3\64\3\64\3\64\3\65\3\65\3\65\3\66\3\66\3\66\3\66\3\67\3\67\38\38"+
		"\39\39\79\u0159\n9\f9\169\u015c\139\3:\3:\3:\7:\u0161\n:\f:\16:\u0164"+
		"\13:\3:\3:\3;\3;\3;\7;\u016b\n;\f;\16;\u016e\13;\3;\3;\3<\3<\3<\3<\3="+
		"\3=\3=\3=\3=\3=\7=\u017c\n=\f=\16=\u017f\13=\3=\5=\u0182\n=\3>\6>\u0185"+
		"\n>\r>\16>\u0186\3?\3?\3?\6?\u018c\n?\r?\16?\u018d\3@\6@\u0191\n@\r@\16"+
		"@\u0192\3@\3@\7@\u0197\n@\f@\16@\u019a\13@\3@\5@\u019d\n@\3@\3@\6@\u01a1"+
		"\n@\r@\16@\u01a2\3@\5@\u01a6\n@\3@\6@\u01a9\n@\r@\16@\u01aa\3@\3@\5@\u01af"+
		"\n@\3A\3A\3A\6A\u01b4\nA\rA\16A\u01b5\3A\3A\7A\u01ba\nA\fA\16A\u01bd\13"+
		"A\3A\5A\u01c0\nA\3A\3A\3A\3A\6A\u01c6\nA\rA\16A\u01c7\3A\5A\u01cb\nA\3"+
		"A\3A\3A\6A\u01d0\nA\rA\16A\u01d1\3A\3A\5A\u01d6\nA\3B\3B\5B\u01da\nB\3"+
		"B\6B\u01dd\nB\rB\16B\u01de\3C\3C\5C\u01e3\nC\3C\6C\u01e6\nC\rC\16C\u01e7"+
		"\3D\3D\3D\3D\5D\u01ee\nD\3D\3D\3D\3D\5D\u01f4\nD\3E\3E\3E\3E\3E\3E\3E"+
		"\3E\3E\3E\3E\5E\u0201\nE\3F\3F\3F\3F\3F\3G\3G\3G\3G\3G\6G\u020d\nG\rG"+
		"\16G\u020e\3G\3G\3H\3H\3I\3I\3J\3J\3J\3J\3J\3J\3J\3J\3J\3K\3K\3K\3K\3"+
		"K\3K\7K\u0226\nK\fK\16K\u0229\13K\3K\3K\7K\u022d\nK\fK\16K\u0230\13K\3"+
		"K\3K\7K\u0234\nK\fK\16K\u0237\13K\3K\3K\7K\u023b\nK\fK\16K\u023e\13K\5"+
		"K\u0240\nK\3K\3K\3K\5K\u0245\nK\3K\3K\3L\3L\3L\7L\u024c\nL\fL\16L\u024f"+
		"\13L\3L\3L\3\u017d\2M\3\3\5\4\7\5\t\6\13\7\r\b\17\t\21\n\23\13\25\f\27"+
		"\r\31\16\33\17\35\20\37\21!\22#\23%\24\'\25)\26+\27-\30/\31\61\32\63\33"+
		"\65\34\67\359\36;\37= ?!A\"C#E$G%I&K\'M(O)Q*S+U,W-Y.[/]\60_\61a\62c\63"+
		"e\64g\65i\66k\67m8o9q:s;u<w=y\2{>}?\177@\u0081A\u0083\2\u0085\2\u0087"+
		"\2\u0089\2\u008b\2\u008d\2\u008f\2\u0091\2\u0093B\u0095C\u0097D\3\2\22"+
		"\5\2C\\aac|\6\2\62;C\\aac|\4\2$$^^\4\2))^^\4\2ZZzz\4\2GGgg\4\2--//\4\2"+
		"RRrr\f\2$$))^^cdhhppttvvxx||\3\2\62\64\3\2\62;\5\2\62;CHch\6\2\f\f\17"+
		"\17??]]\4\2\f\f\17\17\5\2\f\f\17\17]]\4\3\f\f\17\17\2\u0276\2\3\3\2\2"+
		"\2\2\5\3\2\2\2\2\7\3\2\2\2\2\t\3\2\2\2\2\13\3\2\2\2\2\r\3\2\2\2\2\17\3"+
		"\2\2\2\2\21\3\2\2\2\2\23\3\2\2\2\2\25\3\2\2\2\2\27\3\2\2\2\2\31\3\2\2"+
		"\2\2\33\3\2\2\2\2\35\3\2\2\2\2\37\3\2\2\2\2!\3\2\2\2\2#\3\2\2\2\2%\3\2"+
		"\2\2\2\'\3\2\2\2\2)\3\2\2\2\2+\3\2\2\2\2-\3\2\2\2\2/\3\2\2\2\2\61\3\2"+
		"\2\2\2\63\3\2\2\2\2\65\3\2\2\2\2\67\3\2\2\2\29\3\2\2\2\2;\3\2\2\2\2=\3"+
		"\2\2\2\2?\3\2\2\2\2A\3\2\2\2\2C\3\2\2\2\2E\3\2\2\2\2G\3\2\2\2\2I\3\2\2"+
		"\2\2K\3\2\2\2\2M\3\2\2\2\2O\3\2\2\2\2Q\3\2\2\2\2S\3\2\2\2\2U\3\2\2\2\2"+
		"W\3\2\2\2\2Y\3\2\2\2\2[\3\2\2\2\2]\3\2\2\2\2_\3\2\2\2\2a\3\2\2\2\2c\3"+
		"\2\2\2\2e\3\2\2\2\2g\3\2\2\2\2i\3\2\2\2\2k\3\2\2\2\2m\3\2\2\2\2o\3\2\2"+
		"\2\2q\3\2\2\2\2s\3\2\2\2\2u\3\2\2\2\2w\3\2\2\2\2{\3\2\2\2\2}\3\2\2\2\2"+
		"\177\3\2\2\2\2\u0081\3\2\2\2\2\u0093\3\2\2\2\2\u0095\3\2\2\2\2\u0097\3"+
		"\2\2\2\3\u0099\3\2\2\2\5\u009b\3\2\2\2\7\u009d\3\2\2\2\t\u00a3\3\2\2\2"+
		"\13\u00a8\3\2\2\2\r\u00ab\3\2\2\2\17\u00af\3\2\2\2\21\u00b5\3\2\2\2\23"+
		"\u00bc\3\2\2\2\25\u00c2\3\2\2\2\27\u00c5\3\2\2\2\31\u00ca\3\2\2\2\33\u00d1"+
		"\3\2\2\2\35\u00d6\3\2\2\2\37\u00da\3\2\2\2!\u00dc\3\2\2\2#\u00df\3\2\2"+
		"\2%\u00e8\3\2\2\2\'\u00ee\3\2\2\2)\u00f5\3\2\2\2+\u00f8\3\2\2\2-\u00fa"+
		"\3\2\2\2/\u00fc\3\2\2\2\61\u0100\3\2\2\2\63\u0106\3\2\2\2\65\u010b\3\2"+
		"\2\2\67\u010f\3\2\2\29\u0111\3\2\2\2;\u0113\3\2\2\2=\u0115\3\2\2\2?\u0117"+
		"\3\2\2\2A\u0119\3\2\2\2C\u011b\3\2\2\2E\u011e\3\2\2\2G\u0122\3\2\2\2I"+
		"\u0124\3\2\2\2K\u0126\3\2\2\2M\u0129\3\2\2\2O\u012c\3\2\2\2Q\u012f\3\2"+
		"\2\2S\u0132\3\2\2\2U\u0135\3\2\2\2W\u0137\3\2\2\2Y\u0139\3\2\2\2[\u013b"+
		"\3\2\2\2]\u013d\3\2\2\2_\u013f\3\2\2\2a\u0142\3\2\2\2c\u0144\3\2\2\2e"+
		"\u0146\3\2\2\2g\u0148\3\2\2\2i\u014b\3\2\2\2k\u014e\3\2\2\2m\u0152\3\2"+
		"\2\2o\u0154\3\2\2\2q\u0156\3\2\2\2s\u015d\3\2\2\2u\u0167\3\2\2\2w\u0171"+
		"\3\2\2\2y\u0181\3\2\2\2{\u0184\3\2\2\2}\u0188\3\2\2\2\177\u01ae\3\2\2"+
		"\2\u0081\u01d5\3\2\2\2\u0083\u01d7\3\2\2\2\u0085\u01e0\3\2\2\2\u0087\u01f3"+
		"\3\2\2\2\u0089\u0200\3\2\2\2\u008b\u0202\3\2\2\2\u008d\u0207\3\2\2\2\u008f"+
		"\u0212\3\2\2\2\u0091\u0214\3\2\2\2\u0093\u0216\3\2\2\2\u0095\u021f\3\2"+
		"\2\2\u0097\u0248\3\2\2\2\u0099\u009a\7=\2\2\u009a\4\3\2\2\2\u009b\u009c"+
		"\7?\2\2\u009c\6\3\2\2\2\u009d\u009e\7d\2\2\u009e\u009f\7t\2\2\u009f\u00a0"+
		"\7g\2\2\u00a0\u00a1\7c\2\2\u00a1\u00a2\7m\2\2\u00a2\b\3\2\2\2\u00a3\u00a4"+
		"\7i\2\2\u00a4\u00a5\7q\2\2\u00a5\u00a6\7v\2\2\u00a6\u00a7\7q\2\2\u00a7"+
		"\n\3\2\2\2\u00a8\u00a9\7f\2\2\u00a9\u00aa\7q\2\2\u00aa\f\3\2\2\2\u00ab"+
		"\u00ac\7g\2\2\u00ac\u00ad\7p\2\2\u00ad\u00ae\7f\2\2\u00ae\16\3\2\2\2\u00af"+
		"\u00b0\7y\2\2\u00b0\u00b1\7j\2\2\u00b1\u00b2\7k\2\2\u00b2\u00b3\7n\2\2"+
		"\u00b3\u00b4\7g\2\2\u00b4\20\3\2\2\2\u00b5\u00b6\7t\2\2\u00b6\u00b7\7"+
		"g\2\2\u00b7\u00b8\7r\2\2\u00b8\u00b9\7g\2\2\u00b9\u00ba\7c\2\2\u00ba\u00bb"+
		"\7v\2\2\u00bb\22\3\2\2\2\u00bc\u00bd\7w\2\2\u00bd\u00be\7p\2\2\u00be\u00bf"+
		"\7v\2\2\u00bf\u00c0\7k\2\2\u00c0\u00c1\7n\2\2\u00c1\24\3\2\2\2\u00c2\u00c3"+
		"\7k\2\2\u00c3\u00c4\7h\2\2\u00c4\26\3\2\2\2\u00c5\u00c6\7v\2\2\u00c6\u00c7"+
		"\7j\2\2\u00c7\u00c8\7g\2\2\u00c8\u00c9\7p\2\2\u00c9\30\3\2\2\2\u00ca\u00cb"+
		"\7g\2\2\u00cb\u00cc\7n\2\2\u00cc\u00cd\7u\2\2\u00cd\u00ce\7g\2\2\u00ce"+
		"\u00cf\7k\2\2\u00cf\u00d0\7h\2\2\u00d0\32\3\2\2\2\u00d1\u00d2\7g\2\2\u00d2"+
		"\u00d3\7n\2\2\u00d3\u00d4\7u\2\2\u00d4\u00d5\7g\2\2\u00d5\34\3\2\2\2\u00d6"+
		"\u00d7\7h\2\2\u00d7\u00d8\7q\2\2\u00d8\u00d9\7t\2\2\u00d9\36\3\2\2\2\u00da"+
		"\u00db\7.\2\2\u00db \3\2\2\2\u00dc\u00dd\7k\2\2\u00dd\u00de\7p\2\2\u00de"+
		"\"\3\2\2\2\u00df\u00e0\7h\2\2\u00e0\u00e1\7w\2\2\u00e1\u00e2\7p\2\2\u00e2"+
		"\u00e3\7e\2\2\u00e3\u00e4\7v\2\2\u00e4\u00e5\7k\2\2\u00e5\u00e6\7q\2\2"+
		"\u00e6\u00e7\7p\2\2\u00e7$\3\2\2\2\u00e8\u00e9\7n\2\2\u00e9\u00ea\7q\2"+
		"\2\u00ea\u00eb\7e\2\2\u00eb\u00ec\7c\2\2\u00ec\u00ed\7n\2\2\u00ed&\3\2"+
		"\2\2\u00ee\u00ef\7t\2\2\u00ef\u00f0\7g\2\2\u00f0\u00f1\7v\2\2\u00f1\u00f2"+
		"\7w\2\2\u00f2\u00f3\7t\2\2\u00f3\u00f4\7p\2\2\u00f4(\3\2\2\2\u00f5\u00f6"+
		"\7<\2\2\u00f6\u00f7\7<\2\2\u00f7*\3\2\2\2\u00f8\u00f9\7\60\2\2\u00f9,"+
		"\3\2\2\2\u00fa\u00fb\7<\2\2\u00fb.\3\2\2\2\u00fc\u00fd\7p\2\2\u00fd\u00fe"+
		"\7k\2\2\u00fe\u00ff\7n\2\2\u00ff\60\3\2\2\2\u0100\u0101\7h\2\2\u0101\u0102"+
		"\7c\2\2\u0102\u0103\7n\2\2\u0103\u0104\7u\2\2\u0104\u0105\7g\2\2\u0105"+
		"\62\3\2\2\2\u0106\u0107\7v\2\2\u0107\u0108\7t\2\2\u0108\u0109\7w\2\2\u0109"+
		"\u010a\7g\2\2\u010a\64\3\2\2\2\u010b\u010c\7\60\2\2\u010c\u010d\7\60\2"+
		"\2\u010d\u010e\7\60\2\2\u010e\66\3\2\2\2\u010f\u0110\7*\2\2\u01108\3\2"+
		"\2\2\u0111\u0112\7+\2\2\u0112:\3\2\2\2\u0113\u0114\7]\2\2\u0114<\3\2\2"+
		"\2\u0115\u0116\7_\2\2\u0116>\3\2\2\2\u0117\u0118\7}\2\2\u0118@\3\2\2\2"+
		"\u0119\u011a\7\177\2\2\u011aB\3\2\2\2\u011b\u011c\7q\2\2\u011c\u011d\7"+
		"t\2\2\u011dD\3\2\2\2\u011e\u011f\7c\2\2\u011f\u0120\7p\2\2\u0120\u0121"+
		"\7f\2\2\u0121F\3\2\2\2\u0122\u0123\7>\2\2\u0123H\3\2\2\2\u0124\u0125\7"+
		"@\2\2\u0125J\3\2\2\2\u0126\u0127\7>\2\2\u0127\u0128\7?\2\2\u0128L\3\2"+
		"\2\2\u0129\u012a\7@\2\2\u012a\u012b\7?\2\2\u012bN\3\2\2\2\u012c\u012d"+
		"\7\u0080\2\2\u012d\u012e\7?\2\2\u012eP\3\2\2\2\u012f\u0130\7?\2\2\u0130"+
		"\u0131\7?\2\2\u0131R\3\2\2\2\u0132\u0133\7\60\2\2\u0133\u0134\7\60\2\2"+
		"\u0134T\3\2\2\2\u0135\u0136\7-\2\2\u0136V\3\2\2\2\u0137\u0138\7/\2\2\u0138"+
		"X\3\2\2\2\u0139\u013a\7,\2\2\u013aZ\3\2\2\2\u013b\u013c\7\61\2\2\u013c"+
		"\\\3\2\2\2\u013d\u013e\7\'\2\2\u013e^\3\2\2\2\u013f\u0140\7\61\2\2\u0140"+
		"\u0141\7\61\2\2\u0141`\3\2\2\2\u0142\u0143\7(\2\2\u0143b\3\2\2\2\u0144"+
		"\u0145\7~\2\2\u0145d\3\2\2\2\u0146\u0147\7\u0080\2\2\u0147f\3\2\2\2\u0148"+
		"\u0149\7>\2\2\u0149\u014a\7>\2\2\u014ah\3\2\2\2\u014b\u014c\7@\2\2\u014c"+
		"\u014d\7@\2\2\u014dj\3\2\2\2\u014e\u014f\7p\2\2\u014f\u0150\7q\2\2\u0150"+
		"\u0151\7v\2\2\u0151l\3\2\2\2\u0152\u0153\7%\2\2\u0153n\3\2\2\2\u0154\u0155"+
		"\7`\2\2\u0155p\3\2\2\2\u0156\u015a\t\2\2\2\u0157\u0159\t\3\2\2\u0158\u0157"+
		"\3\2\2\2\u0159\u015c\3\2\2\2\u015a\u0158\3\2\2\2\u015a\u015b\3\2\2\2\u015b"+
		"r\3\2\2\2\u015c\u015a\3\2\2\2\u015d\u0162\7$\2\2\u015e\u0161\5\u0087D"+
		"\2\u015f\u0161\n\4\2\2\u0160\u015e\3\2\2\2\u0160\u015f\3\2\2\2\u0161\u0164"+
		"\3\2\2\2\u0162\u0160\3\2\2\2\u0162\u0163\3\2\2\2\u0163\u0165\3\2\2\2\u0164"+
		"\u0162\3\2\2\2\u0165\u0166\7$\2\2\u0166t\3\2\2\2\u0167\u016c\7)\2\2\u0168"+
		"\u016b\5\u0087D\2\u0169\u016b\n\5\2\2\u016a\u0168\3\2\2\2\u016a\u0169"+
		"\3\2\2\2\u016b\u016e\3\2\2\2\u016c\u016a\3\2\2\2\u016c\u016d\3\2\2\2\u016d"+
		"\u016f\3\2\2\2\u016e\u016c\3\2\2\2\u016f\u0170\7)\2\2\u0170v\3\2\2\2\u0171"+
		"\u0172\7]\2\2\u0172\u0173\5y=\2\u0173\u0174\7_\2\2\u0174x\3\2\2\2\u0175"+
		"\u0176\7?\2\2\u0176\u0177\5y=\2\u0177\u0178\7?\2\2\u0178\u0182\3\2\2\2"+
		"\u0179\u017d\7]\2\2\u017a\u017c\13\2\2\2\u017b\u017a\3\2\2\2\u017c\u017f"+
		"\3\2\2\2\u017d\u017e\3\2\2\2\u017d\u017b\3\2\2\2\u017e\u0180\3\2\2\2\u017f"+
		"\u017d\3\2\2\2\u0180\u0182\7_\2\2\u0181\u0175\3\2\2\2\u0181\u0179\3\2"+
		"\2\2\u0182z\3\2\2\2\u0183\u0185\5\u008fH\2\u0184\u0183\3\2\2\2\u0185\u0186"+
		"\3\2\2\2\u0186\u0184\3\2\2\2\u0186\u0187\3\2\2\2\u0187|\3\2\2\2\u0188"+
		"\u0189\7\62\2\2\u0189\u018b\t\6\2\2\u018a\u018c\5\u0091I\2\u018b\u018a"+
		"\3\2\2\2\u018c\u018d\3\2\2\2\u018d\u018b\3\2\2\2\u018d\u018e\3\2\2\2\u018e"+
		"~\3\2\2\2\u018f\u0191\5\u008fH\2\u0190\u018f\3\2\2\2\u0191\u0192\3\2\2"+
		"\2\u0192\u0190\3\2\2\2\u0192\u0193\3\2\2\2\u0193\u0194\3\2\2\2\u0194\u0198"+
		"\7\60\2\2\u0195\u0197\5\u008fH\2\u0196\u0195\3\2\2\2\u0197\u019a\3\2\2"+
		"\2\u0198\u0196\3\2\2\2\u0198\u0199\3\2\2\2\u0199\u019c\3\2\2\2\u019a\u0198"+
		"\3\2\2\2\u019b\u019d\5\u0083B\2\u019c\u019b\3\2\2\2\u019c\u019d\3\2\2"+
		"\2\u019d\u01af\3\2\2\2\u019e\u01a0\7\60\2\2\u019f\u01a1\5\u008fH\2\u01a0"+
		"\u019f\3\2\2\2\u01a1\u01a2\3\2\2\2\u01a2\u01a0\3\2\2\2\u01a2\u01a3\3\2"+
		"\2\2\u01a3\u01a5\3\2\2\2\u01a4\u01a6\5\u0083B\2\u01a5\u01a4\3\2\2\2\u01a5"+
		"\u01a6\3\2\2\2\u01a6\u01af\3\2\2\2\u01a7\u01a9\5\u008fH\2\u01a8\u01a7"+
		"\3\2\2\2\u01a9\u01aa\3\2\2\2\u01aa\u01a8\3\2\2\2\u01aa\u01ab\3\2\2\2\u01ab"+
		"\u01ac\3\2\2\2\u01ac\u01ad\5\u0083B\2\u01ad\u01af\3\2\2\2\u01ae\u0190"+
		"\3\2\2\2\u01ae\u019e\3\2\2\2\u01ae\u01a8\3\2\2\2\u01af\u0080\3\2\2\2\u01b0"+
		"\u01b1\7\62\2\2\u01b1\u01b3\t\6\2\2\u01b2\u01b4\5\u0091I\2\u01b3\u01b2"+
		"\3\2\2\2\u01b4\u01b5\3\2\2\2\u01b5\u01b3\3\2\2\2\u01b5\u01b6\3\2\2\2\u01b6"+
		"\u01b7\3\2\2\2\u01b7\u01bb\7\60\2\2\u01b8\u01ba\5\u0091I\2\u01b9\u01b8"+
		"\3\2\2\2\u01ba\u01bd\3\2\2\2\u01bb\u01b9\3\2\2\2\u01bb\u01bc\3\2\2\2\u01bc"+
		"\u01bf\3\2\2\2\u01bd\u01bb\3\2\2\2\u01be\u01c0\5\u0085C\2\u01bf\u01be"+
		"\3\2\2\2\u01bf\u01c0\3\2\2\2\u01c0\u01d6\3\2\2\2\u01c1\u01c2\7\62\2\2"+
		"\u01c2\u01c3\t\6\2\2\u01c3\u01c5\7\60\2\2\u01c4\u01c6\5\u0091I\2\u01c5"+
		"\u01c4\3\2\2\2\u01c6\u01c7\3\2\2\2\u01c7\u01c5\3\2\2\2\u01c7\u01c8\3\2"+
		"\2\2\u01c8\u01ca\3\2\2\2\u01c9\u01cb\5\u0085C\2\u01ca\u01c9\3\2\2\2\u01ca"+
		"\u01cb\3\2\2\2\u01cb\u01d6\3\2\2\2\u01cc\u01cd\7\62\2\2\u01cd\u01cf\t"+
		"\6\2\2\u01ce\u01d0\5\u0091I\2\u01cf\u01ce\3\2\2\2\u01d0\u01d1\3\2\2\2"+
		"\u01d1\u01cf\3\2\2\2\u01d1\u01d2\3\2\2\2\u01d2\u01d3\3\2\2\2\u01d3\u01d4"+
		"\5\u0085C\2\u01d4\u01d6\3\2\2\2\u01d5\u01b0\3\2\2\2\u01d5\u01c1\3\2\2"+
		"\2\u01d5\u01cc\3\2\2\2\u01d6\u0082\3\2\2\2\u01d7\u01d9\t\7\2\2\u01d8\u01da"+
		"\t\b\2\2\u01d9\u01d8\3\2\2\2\u01d9\u01da\3\2\2\2\u01da\u01dc\3\2\2\2\u01db"+
		"\u01dd\5\u008fH\2\u01dc\u01db\3\2\2\2\u01dd\u01de\3\2\2\2\u01de\u01dc"+
		"\3\2\2\2\u01de\u01df\3\2\2\2\u01df\u0084\3\2\2\2\u01e0\u01e2\t\t\2\2\u01e1"+
		"\u01e3\t\b\2\2\u01e2\u01e1\3\2\2\2\u01e2\u01e3\3\2\2\2\u01e3\u01e5\3\2"+
		"\2\2\u01e4\u01e6\5\u008fH\2\u01e5\u01e4\3\2\2\2\u01e6\u01e7\3\2\2\2\u01e7"+
		"\u01e5\3\2\2\2\u01e7\u01e8\3\2\2\2\u01e8\u0086\3\2\2\2\u01e9\u01ea\7^"+
		"\2\2\u01ea\u01f4\t\n\2\2\u01eb\u01ed\7^\2\2\u01ec\u01ee\7\17\2\2\u01ed"+
		"\u01ec\3\2\2\2\u01ed\u01ee\3\2\2\2\u01ee\u01ef\3\2\2\2\u01ef\u01f4\7\f"+
		"\2\2\u01f0\u01f4\5\u0089E\2\u01f1\u01f4\5\u008bF\2\u01f2\u01f4\5\u008d"+
		"G\2\u01f3\u01e9\3\2\2\2\u01f3\u01eb\3\2\2\2\u01f3\u01f0\3\2\2\2\u01f3"+
		"\u01f1\3\2\2\2\u01f3\u01f2\3\2\2\2\u01f4\u0088\3\2\2\2\u01f5\u01f6\7^"+
		"\2\2\u01f6\u0201\5\u008fH\2\u01f7\u01f8\7^\2\2\u01f8\u01f9\5\u008fH\2"+
		"\u01f9\u01fa\5\u008fH\2\u01fa\u0201\3\2\2\2\u01fb\u01fc\7^\2\2\u01fc\u01fd"+
		"\t\13\2\2\u01fd\u01fe\5\u008fH\2\u01fe\u01ff\5\u008fH\2\u01ff\u0201\3"+
		"\2\2\2\u0200\u01f5\3\2\2\2\u0200\u01f7\3\2\2\2\u0200\u01fb\3\2\2\2\u0201"+
		"\u008a\3\2\2\2\u0202\u0203\7^\2\2\u0203\u0204\7z\2\2\u0204\u0205\5\u0091"+
		"I\2\u0205\u0206\5\u0091I\2\u0206\u008c\3\2\2\2\u0207\u0208\7^\2\2\u0208"+
		"\u0209\7w\2\2\u0209\u020a\7}\2\2\u020a\u020c\3\2\2\2\u020b\u020d\5\u0091"+
		"I\2\u020c\u020b\3\2\2\2\u020d\u020e\3\2\2\2\u020e\u020c\3\2\2\2\u020e"+
		"\u020f\3\2\2\2\u020f\u0210\3\2\2\2\u0210\u0211\7\177\2\2\u0211\u008e\3"+
		"\2\2\2\u0212\u0213\t\f\2\2\u0213\u0090\3\2\2\2\u0214\u0215\t\r\2\2\u0215"+
		"\u0092\3\2\2\2\u0216\u0217\7/\2\2\u0217\u0218\7/\2\2\u0218\u0219\7]\2"+
		"\2\u0219\u021a\3\2\2\2\u021a\u021b\5y=\2\u021b\u021c\7_\2\2\u021c\u021d"+
		"\3\2\2\2\u021d\u021e\bJ\2\2\u021e\u0094\3\2\2\2\u021f\u0220\7/\2\2\u0220"+
		"\u0221\7/\2\2\u0221\u023f\3\2\2\2\u0222\u0240\3\2\2\2\u0223\u0227\7]\2"+
		"\2\u0224\u0226\7?\2\2\u0225\u0224\3\2\2\2\u0226\u0229\3\2\2\2\u0227\u0225"+
		"\3\2\2\2\u0227\u0228\3\2\2\2\u0228\u0240\3\2\2\2\u0229\u0227\3\2\2\2\u022a"+
		"\u022e\7]\2\2\u022b\u022d\7?\2\2\u022c\u022b\3\2\2\2\u022d\u0230\3\2\2"+
		"\2\u022e\u022c\3\2\2\2\u022e\u022f\3\2\2\2\u022f\u0231\3\2\2\2\u0230\u022e"+
		"\3\2\2\2\u0231\u0235\n\16\2\2\u0232\u0234\n\17\2\2\u0233\u0232\3\2\2\2"+
		"\u0234\u0237\3\2\2\2\u0235\u0233\3\2\2\2\u0235\u0236\3\2\2\2\u0236\u0240"+
		"\3\2\2\2\u0237\u0235\3\2\2\2\u0238\u023c\n\20\2\2\u0239\u023b\n\17\2\2"+
		"\u023a\u0239\3\2\2\2\u023b\u023e\3\2\2\2\u023c\u023a\3\2\2\2\u023c\u023d"+
		"\3\2\2\2\u023d\u0240\3\2\2\2\u023e\u023c\3\2\2\2\u023f\u0222\3\2\2\2\u023f"+
		"\u0223\3\2\2\2\u023f\u022a\3\2\2\2\u023f\u0238\3\2\2\2\u0240\u0244\3\2"+
		"\2\2\u0241\u0242\7\17\2\2\u0242\u0245\7\f\2\2\u0243\u0245\t\21\2\2\u0244"+
		"\u0241\3\2\2\2\u0244\u0243\3\2\2\2\u0245\u0246\3\2\2\2\u0246\u0247\bK"+
		"\2\2\u0247\u0096\3\2\2\2\u0248\u0249\7%\2\2\u0249\u024d\7#\2\2\u024a\u024c"+
		"\n\17\2\2\u024b\u024a\3\2\2\2\u024c\u024f\3\2\2\2\u024d\u024b\3\2\2\2"+
		"\u024d\u024e\3\2\2\2\u024e\u0250\3\2\2\2\u024f\u024d\3\2\2\2\u0250\u0251"+
		"\bL\2\2\u0251\u0098\3\2\2\2)\2\u015a\u0160\u0162\u016a\u016c\u017d\u0181"+
		"\u0186\u018d\u0192\u0198\u019c\u01a2\u01a5\u01aa\u01ae\u01b5\u01bb\u01bf"+
		"\u01c7\u01ca\u01d1\u01d5\u01d9\u01de\u01e2\u01e7\u01ed\u01f3\u0200\u020e"+
		"\u0227\u022e\u0235\u023c\u023f\u0244\u024d\3\2\3\2";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}