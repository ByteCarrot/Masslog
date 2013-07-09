var __extends = this.__extends || function (d, b) {
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var Datajo;
(function (Datajo) {
    var Action = (function () {
        function Action(action, sender, data) {
            if(data.action !== action) {
                throw new Exception("'" + action + "' action expected");
            }
            if(data.target === undefined || data.target.trim() === '') {
                throw new Exception('Target of the action has not been defined');
            }
            this.sender = sender;
            this.action = data.action;
            this.target = data.target;
        }
        return Action;
    })();    
    ;
    var ActionHandler = (function () {
        function ActionHandler() { }
        ActionHandler.prototype.execute = function (sender, data) {
        };
        return ActionHandler;
    })();    
    ;
    var ActivityIndicator = (function () {
        function ActivityIndicator(configuration) {
            var selector = configuration.activityIndicator;
            if(selector === undefined || selector.trim() === '') {
                this.showMe = function () {
                };
                this.hideMe = function () {
                };
            } else {
                var obj = $(selector);
                obj.hide();
                this.showMe = function () {
                    obj.show();
                };
                this.hideMe = function () {
                    obj.hide();
                };
            }
        }
        ActivityIndicator.prototype.show = function () {
            this.showMe();
        };
        ActivityIndicator.prototype.hide = function () {
            this.hideMe();
        };
        return ActivityIndicator;
    })();    
    var AjaxAction = (function (_super) {
        __extends(AjaxAction, _super);
        function AjaxAction(action, sender, data) {
                _super.call(this, action, sender, data);
            if(data.url === undefined || data.url.trim() === '') {
                throw new Exception('Url has not been defined');
            }
            this.url = data.url;
            this.inject = data.inject === undefined || data.inject.trim() === '' ? 'replaceContent' : data.inject;
            if(!HtmlInjection.exists(this.inject)) {
                throw new Exception('Unknown injection type: ' + this.inject);
            }
            this.confirm = data.confirm;
        }
        AjaxAction.prototype.confirmed = function () {
            return this.confirm === undefined || this.confirm.trim() === '' || confirm(this.confirm);
        };
        return AjaxAction;
    })(Action);    
    var Configuration = (function () {
        function Configuration() { }
        return Configuration;
    })();    
    var ErrorHandler = (function () {
        function ErrorHandler() {
            var _this = this;
            this.debugMode = QueryString.find('datajo') === 'debug';
            $(window).resize(function () {
                return _this.setViewerSize();
            });
        }
        ErrorHandler.prototype.prepareMessage = function (event, xhr, options, error) {
            return '\n\n--- Datajo Ajax Error --- \n' , 'Status:      ' + xhr.status + '\n' + 'Status Text: ' + xhr.statusText + '\n' + 'Response Text:\n' + xhr.responseText + '\n' + '------------------------';
        };
        ErrorHandler.prototype.showError = function (event, xhr, options, error) {
            var contentType = xhr.getResponseHeader('Content-Type');
            var htmlContent = _.isString(contentType) && contentType.toLowerCase().indexOf('text/html') >= 0;
            var body = $('body');
            body.prepend('<div id="datajo-debug" style="font-size:.9em;padding:0px;z-index:10000;position:absolute;top:0px;left:0px;width:100%;background-color:#f00;color:#fff">' + '<table style="margin:0px;padding:0px;width:100%;border:0px;">' + '<tr>' + '<td style="height:1px; padding:10px 20px 0px 20px;">' + '<button style="float:right;font-weight:bold;color:#fff;background-color:#f00;border:0px;">x</button>' + '<h3 style="margin:0px;padding:0px;">Datajo Ajax Error</h3>' + 'Status: ' + xhr.status + '<br />' + 'Status Text: ' + xhr.statusText + '<br>' + 'Headers: ' + '<div style="margin-left:30px;">' + (xhr.getAllResponseHeaders()).replace(/\n/g, '<br />') + '</div>' + '</td>' + '</tr>' + '<tr>' + '<td style="padding: 20px;">' + '<iframe style="width:100%;border:solid 1px #f00;"></iframe>' + '</td>' + '</tr>' + '</table>' + '</div>');
            var viewer = $('#datajo-debug');
            var button = viewer.find('button').click(function () {
                viewer.replaceWith('');
            });
            var iframe = viewer.find('iframe');
            var doc = iframe.get(0).contentDocument;
            doc.open();
            doc.write(xhr.responseText);
            doc.close();
            this.setViewerSize();
        };
        ErrorHandler.prototype.setViewerSize = function () {
            var viewer = $('#datajo-debug');
            if(viewer === undefined) {
                return;
            }
            var wh = $(window).outerHeight();
            viewer.height(wh);
            viewer.find('table').height(wh);
            viewer.find('iframe').height(wh - viewer.find('td:first').height() - 60);
        };
        ErrorHandler.prototype.onAjaxError = function (event, xhr, options, error) {
            if(event.type !== 'ajaxError') {
                return;
            }
            if(this.debugMode) {
                this.showError(event, xhr, options, error);
            }
            if(console === undefined) {
                return;
            }
            console.error(this.prepareMessage(event, xhr, options, error));
        };
        return ErrorHandler;
    })();    
    var EventName = (function () {
        function EventName() { }
        EventName.events = {
            click: function (e) {
                return true;
            },
            load: function (e) {
                return true;
            },
            submit: function (e) {
                return _.normalize(e.tagName) === 'form';
            }
        };
        EventName.extract = function extract(action, element) {
            if(!element) {
                throw new Exception('HTML element has not been provided');
            }
            var event = _.isString(action.event) ? _.normalize(action.event) : 'click';
            if(event === '') {
                event = 'click';
            }
            if(this.events[event] === undefined) {
                throw new Exception('Event ' + event + ' has not been recognized');
            }
            if(this.events[event](element)) {
                return event;
            }
            throw new Exception('Event "' + event + '" is not supported by "' + element.tagName + '" tag.');
        };
        return EventName;
    })();
    Datajo.EventName = EventName;    
    var Exception = (function () {
        function Exception(message) {
            this.message = message;
        }
        Exception.prototype.toString = function () {
            return 'Datajo exception: ' + this.message;
        };
        return Exception;
    })();    
    var GetAction = (function (_super) {
        __extends(GetAction, _super);
        function GetAction(sender, data) {
                _super.call(this, 'get', sender, data);
        }
        return GetAction;
    })(AjaxAction);    
    ;
    var GetActionHandler = (function (_super) {
        __extends(GetActionHandler, _super);
        function GetActionHandler() {
            _super.apply(this, arguments);

        }
        GetActionHandler.prototype.execute = function (sender, data) {
            var _this = this;
            var action = new GetAction(sender, data);
            if(action.confirmed()) {
                $.get(action.url, function (html) {
                    _this.onSuccess(action, html);
                });
            }
        };
        GetActionHandler.prototype.onSuccess = function (action, html) {
            HtmlInjection.inject(action, html);
        };
        return GetActionHandler;
    })(ActionHandler);    
    ;
    var HideAction = (function (_super) {
        __extends(HideAction, _super);
        function HideAction(sender, data) {
                _super.call(this, 'hide', sender, data);
            this.duration = data.duration;
            this.easing = data.easing;
        }
        return HideAction;
    })(Action);    
    ;
    var HideActionHandler = (function (_super) {
        __extends(HideActionHandler, _super);
        function HideActionHandler() {
            _super.apply(this, arguments);

        }
        HideActionHandler.prototype.execute = function (sender, data) {
            var action = new HideAction(sender, data);
            var target = TargetResolver.resolve(action);
            target.hide(action.duration === undefined ? 200 : action.duration, action.easing === undefined ? 'linear' : action.easing);
        };
        return HideActionHandler;
    })(ActionHandler);    
    ;
    var HtmlInjection = (function () {
        function HtmlInjection() { }
        HtmlInjection.injections = {
            replaceContent: function (t, d) {
                t.html(d);
            },
            afterContent: function (t, d) {
                t.append(d);
            },
            beforeContent: function (t, d) {
                t.prepend(d);
            },
            replaceTarget: function (t, d) {
                t.replaceWith(d);
            },
            beforeTarget: function (t, d) {
                t.before(d);
            },
            afterTarget: function (t, d) {
                t.after(d);
            }
        };
        HtmlInjection.exists = function exists(injection) {
            return HtmlInjection.injections[injection] !== undefined;
        };
        HtmlInjection.inject = function inject(action, html) {
            if(!HtmlInjection.exists(action.inject)) {
                throw new Exception('Unknown injection type: ' + action.inject);
            }
            HtmlInjection.injections[action.inject](TargetResolver.resolve(action), html);
        };
        return HtmlInjection;
    })();    
    var PostAction = (function (_super) {
        __extends(PostAction, _super);
        function PostAction(sender, data) {
                _super.call(this, 'post', sender, data);
            if(data.form === undefined || _.normalize(data.form) === '') {
                throw new Exception('Form has not been defined');
            }
            if(_.normalize(data.form) === '_self') {
                this.form = this.sender;
            } else {
                this.form = $(data.form).get(0);
            }
            if(this.form.tagName.toLowerCase() !== 'form') {
                throw new Exception("Element identified by '" + data.form + "' selector is not a form");
            }
            this.validate = data.validate !== undefined && _.isBool(data.jqvalidate) ? data.jqvalidate : true;
        }
        return PostAction;
    })(AjaxAction);    
    ;
    var PostActionHandler = (function (_super) {
        __extends(PostActionHandler, _super);
        function PostActionHandler() {
            _super.apply(this, arguments);

        }
        PostActionHandler.prototype.execute = function (sender, data) {
            var _this = this;
            var action = new PostAction(sender, data);
            var form = $(action.form);
            if(action.validate && $.validator !== undefined && !form.valid()) {
                return;
            }
            if(action.confirmed()) {
                $.post(action.url, form.serializeArray(), function (html) {
                    _this.onSuccess(action, html);
                });
            }
        };
        PostActionHandler.prototype.onSuccess = function (action, html) {
            HtmlInjection.inject(action, html);
        };
        return PostActionHandler;
    })(ActionHandler);    
    ;
    var QueryString = (function () {
        function QueryString() { }
        QueryString.find = function find(key) {
            var result = {
            }, queryString = location.search.substring(1), re = /([^&=]+)=([^&]*)/g, m;
            while(m = re.exec(queryString)) {
                if(decodeURIComponent(m[1]) === key) {
                    return decodeURIComponent(m[2]);
                }
            }
            return undefined;
        };
        return QueryString;
    })();    
    var Repository = (function () {
        function Repository() { }
        Repository.prototype.findNotConfiguredElements = function () {
            var result = new Array();
            $('[data-jo]').each(function (i, item) {
                if((item).datajo === undefined) {
                    result.push(item);
                }
            });
            return result;
        };
        return Repository;
    })();    
    ;
    var Runner = (function () {
        function Runner() {
            var _this = this;
            $(document).ajaxStart(function () {
                return _this.onAjaxStart();
            });
            $(document).ajaxComplete(function () {
                return _this.onAjaxComplete();
            });
            this.errorHandler = new ErrorHandler();
            $(document).ajaxError(function (e, x, o, err) {
                return _this.errorHandler.onAjaxError(e, x, o, err);
            });
            this.repo = new Repository();
            this.config = this.getConfiguration();
            this.handlers = {
                'show': new ShowActionHandler(),
                'hide': new HideActionHandler(),
                'get': new GetActionHandler(),
                'post': new PostActionHandler()
            };
            this.activityIndicator = new ActivityIndicator(this.config);
            this.update();
        }
        Runner.prototype.getConfiguration = function () {
            var configuration = $(document.body).data('jo-config');
            if(configuration === undefined) {
                return new Configuration();
            }
            return configuration;
        };
        Runner.prototype.update = function () {
            var _this = this;
            var elements = this.repo.findNotConfiguredElements();
            for(var i in elements) {
                var element = elements[i];
                element.datajo = this.findData(element);
                for(var event in element.datajo) {
                    $(element).on(event, function (e) {
                        return _this.onevent(e);
                    });
                    if(event == 'load') {
                        $(element).trigger('load');
                    }
                }
            }
            ;
            if($.validator !== undefined && $.validator.unobtrusive !== undefined) {
                $.validator.unobtrusive.parse('form');
            }
        };
        Runner.prototype.onAjaxStart = function () {
            this.activityIndicator.show();
        };
        Runner.prototype.onAjaxComplete = function () {
            this.update();
            this.activityIndicator.hide();
        };
        Runner.prototype.findData = function (element) {
            for(var i in element.attributes) {
                var attribute = element.attributes[i];
                if(attribute === undefined || attribute.name !== 'data-jo') {
                    continue;
                }
                var obj = JSON.parse(attribute.value);
                var data = {
                };
                if(_.isArray(obj)) {
                    for(var j in obj) {
                        var event = EventName.extract(obj[j], element);
                        if(data[event] === undefined) {
                            data[event] = [];
                        }
                        data[event].push(obj[j]);
                    }
                } else {
                    data[EventName.extract(obj, element)] = [
                        obj
                    ];
                }
                return data;
            }
            return undefined;
        };
        Runner.prototype.onevent = function (event) {
            event.preventDefault();
            var t = event.target;
            while(t.datajo === undefined) {
                t = $(t).parent().get(0);
            }
            var data = t.datajo[event.type];
            for(var i in data) {
                (this.handlers[data[i].action]).execute(t, data[i]);
            }
        };
        return Runner;
    })();    
    ;
    var ShowAction = (function (_super) {
        __extends(ShowAction, _super);
        function ShowAction(sender, data) {
                _super.call(this, 'show', sender, data);
            this.duration = data.duration;
            this.easing = data.easing;
        }
        return ShowAction;
    })(Action);    
    ;
    var ShowActionHandler = (function (_super) {
        __extends(ShowActionHandler, _super);
        function ShowActionHandler() {
            _super.apply(this, arguments);

        }
        ShowActionHandler.prototype.execute = function (sender, data) {
            var action = new ShowAction(sender, data);
            var target = TargetResolver.resolve(action);
            target.show(action.duration === undefined ? 200 : action.duration, action.easing === undefined ? 'linear' : action.easing);
        };
        return ShowActionHandler;
    })(ActionHandler);    
    ;
    var TargetResolver = (function () {
        function TargetResolver() { }
        TargetResolver.resolvers = {
            _parent: function (a) {
                return $(a.sender.parentNode);
            },
            _self: function (a) {
                return $(a.sender);
            }
        };
        TargetResolver.resolve = function resolve(action) {
            if(TargetResolver.resolvers[action.target] !== undefined) {
                return TargetResolver.resolvers[action.target](action);
            }
            return $(action.target);
        };
        return TargetResolver;
    })();    
    var _ = (function () {
        function _() { }
        _.isArray = function isArray(data) {
            return Object.prototype.toString.call(data) === '[object Array]';
        };
        _.isString = function isString(data) {
            return Object.prototype.toString.call(data) == '[object String]';
        };
        _.isBool = function isBool(data) {
            return Object.prototype.toString.call(data) == '[object Boolean]';
        };
        _.normalize = function normalize(data) {
            return data.trim().toLowerCase();
        };
        return _;
    })();
    Datajo._ = _;    
    $(function () {
        var runner = new Runner();
    });
})(Datajo || (Datajo = {}));
//@ sourceMappingURL=datajo.js.map
