﻿/*
BSD License

Copyright (c) 2013, Kazunori Sakamoto
Copyright (c) 2016, Alexander Alexeev
Copyright (c) 2019, Evan Dickinson
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions
are met:

1. Redistributions of source code must retain the above copyright
   notice, this list of conditions and the following disclaimer.
2. Redistributions in binary form must reproduce the above copyright
   notice, this list of conditions and the following disclaimer in the
   documentation and/or other materials provided with the distribution.
3. Neither the NAME of Rainer Schuster nor the NAMEs of its contributors
   may be used to endorse or promote products derived from this software
   without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
"AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

This grammar file derived from:

    Lua 5.3 Reference Manual
    http://www.lua.org/manual/5.3/manual.html

    Lua 5.2 Reference Manual
    http://www.lua.org/manual/5.2/manual.html

    Lua 5.1 grammar written by Nicolai Mainiero
    http://www.antlr3.org/grammar/1178608849736/Lua.g
*/

grammar ExperimentalIncompleteLua;

chunk
    : block EOF
    ;

incompleteChunk
    : incompleteBlock EOF
    ;

block
    : stat* retstat?
    ;

incompleteBlock
    : stat* incompleteStat
    | stat* incompleteRetstat
    ;

stat
    : ';'
    | varlist '=' explist
    | functioncallStatement
    | label
    | 'break'
    | 'goto' NAME
    | 'do' block 'end'
    | 'while' exp 'do' block 'end'
    | 'repeat' block 'until' exp
    | 'if' exp 'then' block ('elseif' exp 'then' block)* ('else' block)? 'end'
    | 'for' NAME '=' exp ',' exp (',' exp)? 'do' block 'end'
    | 'for' namelist 'in' explist 'do' block 'end'
    | 'function' funcname funcbody
    | 'local' 'function' NAME funcbody
    | 'local' namelist ('=' explist)?
    ;

incompleteStat
    : incompleteVarlist | varlist '=' incompleteExplist
    | incompleteFunctioncallStatement
    | 'goto' incompleteName
    | 'do' incompleteBlock
    | 'while' incompleteExp | 'while' exp 'do' incompleteBlock
    | 'repeat' incompleteBlock | 'repeat' block 'until' incompleteExp
    | 'if' incompleteExp | 'if' exp 'then' incompleteBlock | 'if' exp 'then' block incompleteElse
    | 'for' incompleteName | 'for' NAME '=' incompleteExp | 'for' NAME '=' exp ',' incompleteExp
    | 'for' NAME '=' exp ',' exp ',' incompleteExp | | 'for' NAME '=' exp ',' exp (',' exp)? 'do' incompleteBlock
    | 'function' incompleteFuncname | 'function' funcname incompleteFuncbody
    | 'local' 'function' incompleteName | 'local' 'function' NAME incompleteFuncbody 
    | 'local' incompleteNamelist | 'local' namelist '=' incompleteExplist
    ;

incompleteElse
    : ('elseif' exp 'then' block)* 'elseif' incompleteExp | 'elseif' exp 'then' incompleteBlock
    | ('elseif' exp 'then' block)* 'else' incompleteBlock
    ;

retstat
    : 'return' explist? ';'?
    ;

incompleteRetstat
    : 'return' incompleteExplist
    ;

label
    : '::' NAME '::'
    ;

funcname
    : NAME ('.' NAME)* (':' NAME)?
    ;

incompleteFuncname
    : (NAME '.')* incompleteName
    | NAME ('.' NAME)* ':' incompleteName
    ;

varlist
    : var (',' var)*
    ;

incompleteVarlist
    :  (var ',')* incompleteVar
    ;

namelist
    : NAME (',' NAME)*
    ;

incompleteNamelist
    : (NAME ',')* incompleteName
    ;

explist
    : exp (',' exp)*
    ;

incompleteExplist
    : (exp ',')* incompleteExp
    ;

exp
    : 'nil' | 'false' | 'true'
    | number
    | string
    | '...'
    | functiondef
    | segmentedExp
    | tableconstructor
    | <assoc=right> exp operatorPower exp
    | operatorUnary exp
    | exp operatorMulDivMod exp
    | exp operatorAddSub exp
    | <assoc=right> exp operatorStrcat exp
    | exp operatorComparison exp
    | exp operatorAnd exp
    | exp operatorOr exp
    | exp operatorBitwise exp
    ;

incompleteExp
    : incompleteFunctiondef 
    | incompleteSegmentedExp 
    | incompleteTableconstructor
    | <assoc=right> exp operatorPower incompleteExp 
    | operatorUnary incompleteExp
    | exp operatorMulDivMod incompleteExp 
    | exp operatorAddSub incompleteExp
    | <assoc=right> exp operatorStrcat incompleteExp 
    | exp operatorComparison incompleteExp
    | exp operatorAnd incompleteExp 
    | exp operatorOr incompleteExp
    | exp operatorBitwise incompleteExp
    ;

segmentedExp
    : startSegment extendedSegment*
    ;

incompleteSegmentedExp
    : incompleteStartSegment
    | startSegment extendedSegment* incompleteExtendedSegment
    ;

functioncallStatement
    : startSegment extendedSegment* endCallSegment
    | onlyCallSegment
    ;

incompleteFunctioncallStatement
    : incompleteStartSegment
    | startSegment extendedSegment* incompleteExtendedSegment
    | startSegment extendedSegment* incompleteEndCallSegment
    | incompleteOnlyCallSegment
    ;

onlyCallSegment
    : namedArrayAccess anonFunctioncallOrArray* anonFunctioncall
    | namedFunctioncall anonFunctioncallOrArray* anonFunctioncall
    | namedFunctioncall
    ;

incompleteOnlyCallSegment
    : incompleteNamedArrayAccess
    | namedArrayAccess anonFunctioncallOrArray* incompleteAnonFunctioncallOrArray
    | namedArrayAccess anonFunctioncallOrArray* incompleteAnonFunctioncall
    | incompleteNamedFunctioncall 
    | namedFunctioncall anonFunctioncallOrArray* incompleteAnonFunctioncallOrArray
    | namedFunctioncall anonFunctioncallOrArray* incompleteAnonFunctioncall
    ;

endCallSegment
    : '.' fieldEndCallSegment
    | '.' methodEndCallSegment
    | ':' classMethodEndCallSegment
    ;

incompleteEndCallSegment
    : '.' incompleteFieldEndCallSegment
    | '.' incompleteMethodEndCallSegment
    | ':' incompleteClassMethodEndCallSegment
    ;

fieldEndCallSegment
    : namedArrayAccess anonFunctioncallOrArray* anonFunctioncall
    ;

incompleteFieldEndCallSegment
    : incompleteNamedArrayAccess
    | namedArrayAccess anonFunctioncallOrArray* incompleteAnonFunctioncallOrArray
    | namedArrayAccess anonFunctioncallOrArray* incompleteAnonFunctioncall
    ;

methodEndCallSegment
    : namedFunctioncall 
    | namedFunctioncall anonFunctioncallOrArray* anonFunctioncall
    ;

incompleteMethodEndCallSegment
    : incompleteNamedFunctioncall
    | namedFunctioncall incompleteAnonFunctioncallOrArray* incompleteAnonFunctioncallOrArray
    | namedFunctioncall incompleteAnonFunctioncallOrArray* incompleteAnonFunctioncall
    ;

classMethodEndCallSegment
    : methodEndCallSegment
    ;

incompleteClassMethodEndCallSegment
    : incompleteMethodEndCallSegment
    ;

startSegment
    : variableName
    | namedArrayAccess anonFunctioncallOrArray*
    | namedFunctioncall anonFunctioncallOrArray*
    ;

incompleteStartSegment
    : incompleteVariableName
    | incompleteNamedArrayAccess
    | namedArrayAccess anonFunctioncallOrArray* incompleteAnonFunctioncallOrArray
    | incompleteNamedFunctioncall
    | namedFunctioncall anonFunctioncallOrArray* incompleteAnonFunctioncallOrArray
    ;

variableName
    : NAME
    ;

incompleteVariableName
    : incompleteName
    ;

functionName
    : NAME
    ;

incompleteFunctionName
    : incompleteName
    ;

anonFunctioncallOrArray
    : anonFunctioncall | arrayAccess
    ;

incompleteAnonFunctioncallOrArray
    : incompleteAnonFunctioncall | incompleteArrayAccess
    ;

namedArrayAccess
    : variableName arrayAccess
    ;

incompleteNamedArrayAccess
    : incompleteVariableName
    | variableName incompleteArrayAccess
    ;

extendedSegment
    : '.' fieldSegment
    | '.' methodSegment
    | ':' classMethodSegment
    ;

incompleteExtendedSegment
    : '.' incompleteFieldSegment
    | '.' incompleteMethodSegment
    | ':' incompleteClassMethodSegment
    ;

methodSegment
    : namedFunctioncall anonFunctioncallOrArray*
    ;

incompleteMethodSegment
    : incompleteNamedFunctioncall
    | namedFunctioncall anonFunctioncallOrArray* incompleteAnonFunctioncallOrArray
    ;

classMethodSegment
    : methodSegment
    ;

incompleteClassMethodSegment
    : incompleteMethodSegment
    ;

methodName
    : NAME
    ;

fieldSegment
    : fieldName 
    | namedArrayAccess anonFunctioncallOrArray*
    ;

incompleteFieldSegment
    : incompleteFieldName
    | incompleteNamedArrayAccess
    | namedArrayAccess anonFunctioncallOrArray* incompleteAnonFunctioncallOrArray
    ;

fieldName
    : NAME
    ;

incompleteFieldName
    : incompleteName
    ;

arrayAccess
    : '[' arrayArg ']'
    ;

incompleteArrayAccess
    : '[' incompleteArrayArg
    ;

previxedFunctioncall
    : '.'
    ;

functioncall
    : namedFunctioncall | anonFunctioncall
    ;

namedFunctioncall
    : NAME args
    ;

incompleteNamedFunctioncall
    : incompleteName
    | NAME incompleteArgs
    ;

anonFunctioncall
    : args
    ;

incompleteAnonFunctioncall
    : incompleteArgs
    ;


varOrExp
    : var | '(' exp ')'
    ;

incompleteVarOrExp
    : incompleteVar | '(' incompleteExp
    ;

var
    : (NAME | '(' exp ')' varSuffix) varSuffix*
    ;

incompleteVar
    : incompleteName | '(' incompleteExp | '(' exp ')' incompleteVarSuffix
    | (varName | '(' exp ')' varSuffix) varSuffix* incompleteVarSuffix
    ;

varName
    : NAME
    ;

varSuffix
    : nameAndArgs* ('[' arrayArg ']' | '.' memberName)
    ;



memberName
    : NAME
    ;

arrayArg
    : exp
    ;

incompleteArrayArg
    : incompleteExp
    ;

incompleteVarSuffix
    : nameAndArgs* incompleteNameAndArgs
    | nameAndArgs* ('[' incompleteExp | '.' incompleteName)
    ;

nameAndArgs
    : (':' NAME)? args
    ;

incompleteNameAndArgs
    : ':' incompleteName | (':' NAME)? incompleteArgs
    ;

/*
var
    : NAME | prefixexp '[' exp ']' | prefixexp '.' NAME
    ;

prefixexp
    : var | functioncall | '(' exp ')'
    ;

functioncall
    : prefixexp args | prefixexp ':' NAME args 
    ;
*/

args
    : '(' explist? ')' | tableconstructor | string
    ;



incompleteArgs
    : '(' incompleteExplist | incompleteTableconstructor | incompleteString
    ;


functiondef
    : 'function' funcbody
    ;

incompleteFunctiondef
    : 'function' incompleteFuncbody
    ;

funcbody
    : '(' parlist? ')' block 'end'
    ;

incompleteFuncbody
    : '(' incompleteParlist | '(' parlist? ')' incompleteBlock
    ;

parlist
    : namelist (',' '...')? | '...'
    ;

incompleteParlist
    : incompleteNamelist
    ;

tableconstructor
    : '{' fieldlist? '}'
    ;

incompleteTableconstructor
    : '{' incompleteFieldlist
    ;

fieldlist
    : field (fieldsep field)* fieldsep?
    ;

incompleteFieldlist
    : (field fieldsep)* incompleteField
    ;

field
    : '[' exp ']' '=' exp | NAME '=' exp | exp
    ;

incompleteField
    : '[' incompleteExp | '[' exp ']' '=' incompleteExp | incompleteName | NAME '=' incompleteExp | incompleteExp
    ;

fieldsep
    : ',' | ';'
    ;

operatorOr 
    : 'or';

operatorAnd 
    : 'and';

operatorComparison 
    : '<' | '>' | '<=' | '>=' | '~=' | '==';

operatorStrcat
    : '..';

operatorAddSub
    : '+' | '-';

operatorMulDivMod
    : '*' | '/' | '%' | '//';

operatorBitwise
    : '&' | '|' | '~' | '<<' | '>>';

operatorUnary
    : 'not' | '#' | '-' | '~';

operatorPower
    : '^';

number
    : INT | HEX | FLOAT | HEX_FLOAT
    ;

string
    : NORMALSTRING | CHARSTRING | LONGSTRING
    ;

incompleteString
    : NORMALSTRING | CHARSTRING | LONGSTRING
    ;

incompleteName
    : NAME
    ;

// LEXER

NAME
    : [a-zA-Z_][a-zA-Z_0-9]*
    ;

NORMALSTRING
    : '"' ( EscapeSequence | ~('\\'|'"') )* '"' 
    ;

CHARSTRING
    : '\'' ( EscapeSequence | ~('\''|'\\') )* '\''
    ;

LONGSTRING
    : '[' NESTED_STR ']'
    ;

fragment
NESTED_STR
    : '=' NESTED_STR '='
    | '[' .*? ']'
    ;

INT
    : Digit+
    ;

HEX
    : '0' [xX] HexDigit+
    ;

FLOAT
    : Digit+ '.' Digit* ExponentPart?
    | '.' Digit+ ExponentPart?
    | Digit+ ExponentPart
    ;

HEX_FLOAT
    : '0' [xX] HexDigit+ '.' HexDigit* HexExponentPart?
    | '0' [xX] '.' HexDigit+ HexExponentPart?
    | '0' [xX] HexDigit+ HexExponentPart
    ;

fragment
ExponentPart
    : [eE] [+-]? Digit+
    ;

fragment
HexExponentPart
    : [pP] [+-]? Digit+
    ;

fragment
EscapeSequence
    : '\\' [abfnrtvz"'\\]
    | '\\' '\r'? '\n'
    | DecimalEscape
    | HexEscape
    | UtfEscape
    ;
    
fragment
DecimalEscape
    : '\\' Digit
    | '\\' Digit Digit
    | '\\' [0-2] Digit Digit
    ;
    
fragment
HexEscape
    : '\\' 'x' HexDigit HexDigit
    ;

fragment
UtfEscape
    : '\\' 'u{' HexDigit+ '}'
    ;

fragment
Digit
    : [0-9]
    ;

fragment
HexDigit
    : [0-9a-fA-F]
    ;

COMMENT
    : '--[' NESTED_STR ']' -> channel(HIDDEN)
    ;
    
LINE_COMMENT
    : '--'
    (                                               // --
    | '[' '='*                                      // --[==
    | '[' '='* ~('='|'['|'\r'|'\n') ~('\r'|'\n')*   // --[==AA
    | ~('['|'\r'|'\n') ~('\r'|'\n')*                // --AAA
    ) ('\r\n'|'\r'|'\n'|EOF)
    -> channel(HIDDEN)
    ;
    
WS  
    : [ \t\u000C\r\n]+ -> skip
    ;

SHEBANG
    : '#' '!' ~('\n'|'\r')* -> channel(HIDDEN)
    ;
