CodeMirror.defineMode("rules", function () {
    var curPunc;

    function wordRegexp(words) {
        return new RegExp("^(?:" + words.join("|") + ")$", "i");
    }

    var keywords = wordRegexp([
        ('HEADER'), ('URL'), ('IGNORE'), ('LOG'), ('MACHINE'),
        ('URL'), ('SKIP'), ('REQUEST'), ('RESPONSE'), ('ACTIVITY'),
        ('SERVER'), ('VARIABLES'), ('BODY'), ('STATUS'), ('CODE'), ('ROUTE'),
        ('DATA'), ('SIZE')]);

    var blocks = wordRegexp([('WHEN'), ('THEN')]);

    var logicalop = wordRegexp([('OR'), ('AND')]);

    var operatorChars = /[=\!]/;

    function tokenBase(stream, state) {
        var ch = stream.next();
        curPunc = null;

        if (ch == "'") {
            state.tokenize = tokenLiteral(ch);
            return state.tokenize(stream, state);
        }
        else if (/[\(\);]/.test(ch)) {
            curPunc = ch;
            return null;
        }
        else if (operatorChars.test(ch)) {
            stream.eatWhile(operatorChars);
            return null;
        }
        else {
            stream.eatWhile(/[\w\d]/);
            var word = stream.current();
            if (keywords.test(word))
                return "keyword";
            else if (blocks.test(word))
                return "block";
            else if (logicalop.test(word))
                return "logicalop";
            else
                return null;
        }
    }

    function tokenLiteral(quote) {
        return function (stream, state) {
            var escaped = false, ch;
            while ((ch = stream.next()) != null) {
                if (ch == quote && !escaped) {
                    state.tokenize = tokenBase;
                    break;
                }
                escaped = !escaped && ch == "\\";
            }
            return "string";
        };
    }

    return {
        startState: function (base) {
            return { tokenize: tokenBase, context: null, indent: 0, col: 0 };
        },

        token: function (stream, state) {
            return stream.eatSpace() ? null : state.tokenize(stream, state);
        }
    };
});

CodeMirror.defineMIME("text/x-rules", "rules");
