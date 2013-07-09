/// <reference path="jquery.d.ts" />

module Datajo {

    class Action {
        constructor(action: string, sender: Element, data: any) {
            if (data.action !== action) {
                throw new Exception("'" + action + "' action expected");
            }
            if (data.target === undefined || data.target.trim() === '') {
                throw new Exception('Target of the action has not been defined');
            }
            this.sender = sender;
            this.action = data.action;
            this.target = data.target;
        }
        sender: Element;
        action: string;
        target: string;
    };

    class ActionHandler {
        execute(sender: Element, data: any) { }
    };

    class ActivityIndicator {
        private showMe: () => void;
        private hideMe: () => void;
        constructor(configuration: Configuration) {
            var selector = configuration.activityIndicator;
            if (selector === undefined || selector.trim() === '') {
                this.showMe = () => { };
                this.hideMe = () => { }
            } else {
                var obj = $(selector);
                obj.hide();
                this.showMe = () => { obj.show(); };
                this.hideMe = () => { obj.hide(); };
            }
        }
        public show() {
            this.showMe();
        }
        public hide() {
            this.hideMe();
        }
    }

    class AjaxAction extends Action {
        url: string;
        inject: string;
        confirm: string;
        constructor(action: string, sender: Element, data: any) {
            super(action, sender, data);
            if (data.url === undefined || data.url.trim() === '') {
                throw new Exception('Url has not been defined');
            }
            this.url = data.url;
            this.inject = data.inject === undefined || data.inject.trim() === '' ? 'replaceContent' : data.inject;
            if (!HtmlInjection.exists(this.inject)) {
                throw new Exception('Unknown injection type: ' + this.inject);
            }
            this.confirm = data.confirm;
        }
        public confirmed(): bool {
            return this.confirm === undefined || this.confirm.trim() === '' || confirm(this.confirm);
        }
    }

    class Configuration {
        activityIndicator: string;
    }

    class ErrorHandler {
        private debugMode: bool;
        constructor() {
            this.debugMode = QueryString.find('datajo') === 'debug';
            $(window).resize(() => this.setViewerSize());
        }
        private prepareMessage(event: any, xhr: any, options: any, error: any): string {
            return '\n\n--- Datajo Ajax Error --- \n',
                'Status:      ' + xhr.status + '\n' +
                'Status Text: ' + xhr.statusText + '\n' +
                'Response Text:\n' + xhr.responseText + '\n' +
                '------------------------';
        }
        private showError(event: any, xhr: any, options: any, error: any) {
            var contentType: string = xhr.getResponseHeader('Content-Type');
            var htmlContent = _.isString(contentType) && contentType.toLowerCase().indexOf('text/html') >= 0;

            var body = $('body');

            body.prepend(
                '<div id="datajo-debug" style="font-size:.9em;padding:0px;z-index:10000;position:absolute;top:0px;left:0px;width:100%;background-color:#f00;color:#fff">' +
                    '<table style="margin:0px;padding:0px;width:100%;border:0px;">' +
                        '<tr>' +
                            '<td style="height:1px; padding:10px 20px 0px 20px;">' +
                                '<button style="float:right;font-weight:bold;color:#fff;background-color:#f00;border:0px;">x</button>' +
                                '<h3 style="margin:0px;padding:0px;">Datajo Ajax Error</h3>' +
                                'Status: ' + xhr.status + '<br />' +
                                'Status Text: ' + xhr.statusText + '<br>' +
                                'Headers: ' +
                                '<div style="margin-left:30px;">' +
                                    (<string>xhr.getAllResponseHeaders()).replace(/\n/g, '<br />') +
                                '</div>' +
                            '</td>' +
                        '</tr>' +
                        '<tr>' +
                            '<td style="padding: 20px;">' +
                                '<iframe style="width:100%;border:solid 1px #f00;"></iframe>' +
                            '</td>' +
                        '</tr>' +
                    '</table>' +
                '</div>');

            var viewer = $('#datajo-debug');
            var button = viewer.find('button').click(() => {
                viewer.replaceWith('');
            });
            var iframe = viewer.find('iframe')
            var doc = iframe.get(0).contentDocument;
            doc.open();
            doc.write(xhr.responseText);
            doc.close();

            this.setViewerSize();
        }
        private setViewerSize() {
            var viewer = $('#datajo-debug');
            if (viewer === undefined) {
                return;
            }

            var wh = $(window).outerHeight();
            viewer.height(wh);
            viewer.find('table').height(wh);
            viewer.find('iframe').height(wh - viewer.find('td:first').height() - 60);
        }
        public onAjaxError(event: any, xhr: any, options: any, error: any): any {
            if (event.type !== 'ajaxError') {
                return;
            }
            if (this.debugMode) {
                this.showError(event, xhr, options, error);
            }
            if (console === undefined) {
                return;
            }
            console.error(this.prepareMessage(event, xhr, options, error));
        }
    }

    export class EventName {
        private static events = {
            click: (e: Element) => { return true },
            load: (e: Element) => { return true },
            submit: (e: Element) => { return _.normalize(e.tagName) === 'form' },
        };
        public static extract(action: any, element: Element): string {
            if (!element) {
                throw new Exception('HTML element has not been provided');
            }

            var event = _.isString(action.event) ? _.normalize(action.event) : 'click';
            if (event === '') {
                event = 'click';
            }

            if (this.events[event] === undefined) {
                throw new Exception('Event ' + event + ' has not been recognized');
            }

            if (this.events[event](element)) {
                return event;
            }

            throw new Exception('Event "' + event + '" is not supported by "' + element.tagName + '" tag.');
        }
    }

    class Exception {
        private message: string;
        constructor(message: string) {
            this.message = message;
        }
        toString() {
            return 'Datajo exception: ' + this.message;
        }
    }

    class GetAction extends AjaxAction {
        constructor(sender: Element, data: any) {
            super('get', sender, data);
        }
    };

    class GetActionHandler extends ActionHandler {
        execute(sender: Element, data: any) {
            var action = new GetAction(sender, data);
            if (action.confirmed()) {
                $.get(action.url, html => { this.onSuccess(action, html); });
            }
        }
        private onSuccess(action: GetAction, html: string) {
            HtmlInjection.inject(action, html);
        }
    };

    class HideAction extends Action {
        constructor(sender: Element, data: any) {
            super('hide', sender, data);
            this.duration = data.duration;
            this.easing = data.easing;
        }
        duration: number;
        easing: string;
    };

    class HideActionHandler extends ActionHandler {
        execute(sender: Element, data: any) {
            var action = new HideAction(sender, data);
            var target = TargetResolver.resolve(action);
            target.hide(
                action.duration === undefined ? 200 : action.duration,
                action.easing === undefined ? 'linear' : action.easing);
        }
    };

    class HtmlInjection {
        private static injections = {
            replaceContent: (t: JQuery, d: any) => { t.html(d); },
            afterContent: (t: JQuery, d: any) => { t.append(d); },
            beforeContent: (t: JQuery, d: any) => { t.prepend(d); },
            replaceTarget: (t: JQuery, d: any) => { t.replaceWith(d); },
            beforeTarget: (t: JQuery, d: any) => { t.before(d); },
            afterTarget: (t: JQuery, d: any) => { t.after(d); }
        };
        static exists(injection: string): bool {
            return HtmlInjection.injections[injection] !== undefined;
        }
        static inject(action: AjaxAction, html: string) {
            if (!HtmlInjection.exists(action.inject)) {
                throw new Exception('Unknown injection type: ' + action.inject);
            }
            HtmlInjection.injections[action.inject](TargetResolver.resolve(action), html);
        }
    }

    class PostAction extends AjaxAction {
        form: Element;
        validate: bool;
        constructor(sender: Element, data: any) {
            super('post', sender, data);
            if (data.form === undefined || _.normalize(data.form) === '') {
                throw new Exception('Form has not been defined');
            }

            if (_.normalize(data.form) === '_self') {
                this.form = this.sender;
            } else {
                this.form = $(data.form).get(0);
            }

            if (this.form.tagName.toLowerCase() !== 'form') {
                throw new Exception("Element identified by '" + data.form + "' selector is not a form");
            }

            this.validate = data.validate !== undefined && _.isBool(data.jqvalidate) ? data.jqvalidate : true;
        }
    };

    class PostActionHandler extends ActionHandler {
        execute(sender: Element, data: any) {
            var action = new PostAction(sender, data);
            var form = $(action.form);
            if (action.validate && $.validator !== undefined && !form.valid()) {
                return;
            }
            if (action.confirmed()) {
                $.post(action.url, form.serializeArray(), html => { this.onSuccess(action, html); });
            }
        }
        private onSuccess(action: PostAction, html: string) {
            HtmlInjection.inject(action, html);
        }
    };

    class QueryString {
        public static find(key: string): string {
            var result = {}, queryString = location.search.substring(1),
            re = /([^&=]+)=([^&]*)/g, m;
            while (m = re.exec(queryString)) {
                if (decodeURIComponent(m[1]) === key) {
                    return decodeURIComponent(m[2]);
                }
            }
            return undefined;
        }
    }

    class Repository {
        public findNotConfiguredElements(): Element[] {
            var result = new Element[];
            $('[data-jo]').each((i, item) => {
                if ((<any>item).datajo === undefined) {
                    result.push(item);
                }
            });
            return result;
        };
    };

    class Runner {
        private repo: Repository;
        private config: Configuration;
        private handlers: any;
        private events: any;
        private activityIndicator: ActivityIndicator;
        private errorHandler: ErrorHandler;
        constructor() {
            $(document).ajaxStart(() => this.onAjaxStart());
            $(document).ajaxComplete(() => this.onAjaxComplete());

            this.errorHandler = new ErrorHandler();
            $(document).ajaxError((e: any, x: any, o: any, err: any) => this.errorHandler.onAjaxError(e, x, o, err));

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

        private getConfiguration(): Configuration {
            var configuration = $(document.body).data('jo-config');
            if (configuration === undefined) {
                return new Configuration();
            }
            return configuration;
        }

        public update() {
            var elements = this.repo.findNotConfiguredElements();
            for (var i in elements) {
                var element = elements[i];
                element.datajo = this.findData(element);
                for (var event in element.datajo) {
                    $(element).on(event, e => this.onevent(e));
                    if (event == 'load') {
                        $(element).trigger('load');
                    }
                }
            };

            // Enable client side validation if Microsoft's Unobtrusive Validation plugin is available
            if ($.validator !== undefined && $.validator.unobtrusive !== undefined) {
                $.validator.unobtrusive.parse('form')
            }
        }

        public onAjaxStart() {
            this.activityIndicator.show();
        }

        public onAjaxComplete() {
            this.update();
            this.activityIndicator.hide();
        }

        private findData(element: Element): any {
            for (var i in element.attributes) {
                var attribute = element.attributes[i];
                if (attribute === undefined || attribute.name !== 'data-jo') {
                    continue;
                }
                var obj = JSON.parse(attribute.value);
                var data = {};
                if (_.isArray(obj)) {
                    for (var j in obj) {
                        var event = EventName.extract(obj[j], element);
                        if (data[event] === undefined) {
                            data[event] = [];
                        }
                        data[event].push(obj[j]);
                    }
                } else {
                    data[EventName.extract(obj, element)] = [obj];
                }
                return data;
            }
            return undefined;
        }

        private onevent(event: any) {
            event.preventDefault();
            var t = event.target;
            while (t.datajo === undefined) {
                t = $(t).parent().get(0);
            }

            var data = t.datajo[event.type];
            for (var i in data) {
                (<ActionHandler>this.handlers[data[i].action]).execute(<Element>t, data[i]);
            }
        }
    };

    class ShowAction extends Action {
        constructor(sender: Element, data: any) {
            super('show', sender, data);
            this.duration = data.duration;
            this.easing = data.easing;
        }
        duration: number;
        easing: string;
    };

    class ShowActionHandler extends ActionHandler {
        execute(sender: Element, data: any) {
            var action = new ShowAction(sender, data);
            var target = TargetResolver.resolve(action);
            target.show(
                action.duration === undefined ? 200 : action.duration,
                action.easing === undefined ? 'linear' : action.easing);
        }
    };

    class TargetResolver {
        private static resolvers = {
            _parent: (a: Action): JQuery => { return $(a.sender.parentNode) },
            _self: (a: Action): JQuery => { return $(a.sender) },
        };
        static resolve(action: Action): JQuery {
            if (TargetResolver.resolvers[action.target] !== undefined) {
                return TargetResolver.resolvers[action.target](action);
            }
            return $(action.target);
        }
    }

    export class _ {
        public static isArray(data: any): bool {
            return Object.prototype.toString.call(data) === '[object Array]';
        }

        public static isString(data: any): bool {
            return Object.prototype.toString.call(data) == '[object String]';
        }

        public static isBool(data: any): bool {
            return Object.prototype.toString.call(data) == '[object Boolean]';
        }

        public static normalize(data: string): string {
            return data.trim().toLowerCase();
        }
    }

    $(function () {
        var runner = new Runner();
    })
}