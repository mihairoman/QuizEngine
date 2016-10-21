function PagerControl() {

    var _infoCount;
    var _infoOnPage;
    var _onPage = 1;
    var _oldInfoCount = 0;
    var _clickedLink = 1;
    var _form;
    var _nrPages;

    init = function (infoCount, infoOnPage, form) {
        _infoCount = parseInt(infoCount);
        _infoOnPage = parseInt(infoOnPage);
        _form = "." + form;

        if (_infoCount > _oldInfoCount) { _oldInfoCount = _infoCount; }
        PopulatePager();
    }

    function PopulatePager() {

        if ((_infoCount == 0) || (_infoCount < _infoOnPage)) {
            _nrPages = 1;
        }
        else if (_infoCount % _infoOnPage != 0) {
            _nrPages = Math.round((_infoCount / _infoOnPage));
        } else {
            _nrPages = Math.round(_infoCount / _infoOnPage);
        }

        // Append the previouse buttons
        if (_nrPages > 1) {
            $(_form).append("<input type='button' class='firstpage'  value='<<'/>  ");
            $(_form).append("<input type='button' class='prevpage'  value='<'/> ");
        }

        if (_nrPages <= 10) {
            if (_nrPages > 1) {
                for (var i = 1; i <= _nrPages; i++) {
                    if (_clickedLink == i && _clickedLink) {
                        $(_form).append("<input type='button' class='pagerindex' style='background-color: #C0C0C0;' value='" + i + "'/>");
                    }
                    else {
                        $(_form).append("<input type='button' style='background-color: #D7DCE0;' class='pagerindex' value='" + i + "'/>");
                    }
                }
            }
        } else {
            var startDisplayPagerStart = 1;
            var endDisplayPagerStart = 4;

            if (_onPage < 4) {
                startDisplayPagerStart = 1;
                endDisplayPagerStart = 4;
            } else {
                $(_form).append("<input type='button' class='pagerindex' value='1'/>");
                //Append the '...' section
                $(_form).append("<a >...</a>");

                if (_onPage < _nrPages - 2) {
                    startDisplayPagerStart = _onPage - 1;
                    endDisplayPagerStart = startDisplayPagerStart + 2;
                }
                else {
                    startDisplayPagerStart = _nrPages - 3;
                    endDisplayPagerStart = _nrPages;
                }
            }

            for (var i = startDisplayPagerStart; i <= endDisplayPagerStart ; i++) {

                if (_clickedLink == i && _clickedLink) {
                    $(_form).append("<input type='button' class='pagerindex' style='background-color: #C0C0C0;' value='" + i + "'/>");
                }
                else {
                    $(_form).append("<input type='button' class='pagerindex' value='" + i + "'/>");
                }
            }

            if (_onPage < _nrPages - 2) {
                //Append the '...' section
                $(_form).append("<a >...</a>");
                $(_form).append("<input type='button' class='pagerindex' value='" + (_nrPages) + "'/>");
            }
        }


        // Append the next buttons
        if (_nrPages > 1) {
            $(_form).append("<input class='nextpage' type='button' value='>'/>");
            $(_form).append("<input class='lastpage' type='button' value='>>'/>");
        }

        BindEvents();
    }

    function BindEvents() {
        $(_form).off('click', '.pagerindex').on('click', '.pagerindex', function (event) {
            event.preventDefault();
            event.stopPropagation();
            _clickedLink = parseInt($(this).val());
            _onPage = parseInt($(this).val());
            TriggerPagerEvent(event);
        });
        //Next and Last events
        $(_form).off('click', '.nextpage').on('click', '.nextpage', function (event) {
            event.preventDefault();
            event.stopPropagation();
            if (_onPage < _nrPages) {
                _clickedLink = parseInt(_clickedLink + 1);
                _onPage = parseInt(_onPage + 1);
                TriggerPagerEvent(event);
            }
        });

        $(_form).off('click', '.lastpage').on('click', '.lastpage', function (event) {
            event.preventDefault();
            event.stopPropagation();

            _clickedLink = parseInt(_nrPages);
            _onPage = parseInt(_nrPages);
            TriggerPagerEvent(event);
        });

        //Prev and first
        $(_form).off('click', '.prevpage').on('click', '.prevpage', function (event) {
            event.preventDefault();
            event.stopPropagation();

            if (_onPage > 1) {
                _clickedLink = parseInt(_clickedLink - 1);
                _onPage = parseInt(_onPage - 1);
                TriggerPagerEvent(event);
            }
        });

        $(_form).off('click', '.firstpage').on('click', '.firstpage', function (event) {
            event.preventDefault();
            event.stopPropagation();
            _clickedLink = 1;
            _onPage = 1;
            TriggerPagerEvent(event);
        });

        function TriggerPagerEvent(event) {
            $.event.trigger({
                type: "pagerIndexChanged",
                onPage: _onPage
            });
        }
    }
    reset = function () {
        if ($(_form).children().length > 0) {
            $(_form).empty();
        }
    }

    questionCount = function () {
        return _oldInfoCount;
    }

    resetClickedLink = function () {
        _clickedLink = 1;
        _onPage = 1;
    }

    return {
        Init: init,
        InfoCount: questionCount,
        Reset: reset,
        ResetLink: resetClickedLink
    }
}