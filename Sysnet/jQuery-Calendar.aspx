<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="jQuery-Calendar.aspx.cs"
    Inherits="TestApplication.Sysnet.jQuery_Calendar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.1/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.14/jquery-ui.min.js"></script>
    <link rel="stylesheet" type="text/css" media="screen" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.14/themes/base/jquery-ui.css" />
    <script type="text/javascript">

        $.maxZIndex = $.fn.maxZIndex = function (opt) {
            /// <summary>
            /// Returns the max zOrder in the document (no parameter)
            /// Sets max zOrder by passing a non-zero number
            /// which gets added to the highest zOrder.
            /// </summary>    
            /// <param name="opt" type="object">
            /// inc: increment value, 
            /// group: selector for zIndex elements to find max for
            /// </param>
            /// <returns type="jQuery" />
            var def = { inc: 10, group: "*" };
            $.extend(def, opt);
            var zmax = 0;
            $(def.group).each(function () {
                var cur = parseInt($(this).css('z-index'));
                zmax = cur > zmax ? cur : zmax;
            });
            if (!this.jquery)
                return zmax;

            return this.each(function () {
                zmax += def.inc;
                $(this).css("z-index", zmax);
            });
        }
    </script>
    <script type="text/javascript">
        $(function () {
            var startDate;
            var endDate;

            var selectCurrentWeek = function () {
                window.setTimeout(function () {
                    $('.week-picker').find('.ui-datepicker-current-day a').addClass('ui-state-active')
                }, 1);
            }

            $('.week-picker').datepicker({
                showOtherMonths: true,
                selectOtherMonths: true,
                onSelect: function (dateText, inst) {
                    var date = $(this).datepicker('getDate');
                    startDate = new Date(date.getFullYear(), date.getMonth(), date.getDate() - date.getDay());
                    endDate = new Date(date.getFullYear(), date.getMonth(), date.getDate() - date.getDay() + 6);
                    var dateFormat = inst.settings.dateFormat || $.datepicker._defaults.dateFormat;
                    $('#startDate').text($.datepicker.formatDate(dateFormat, startDate, inst.settings));
                    $('#endDate').text($.datepicker.formatDate(dateFormat, endDate, inst.settings));
                    $('#week').val($.datepicker.formatDate(dateFormat, startDate, inst.settings) + '-' + $.datepicker.formatDate(dateFormat, endDate, inst.settings));



                    selectCurrentWeek();
                    hideCalendar();
                },
                beforeShowDay: function (date) {
                    var cssClass = '';
                    if (date >= startDate && date <= endDate)
                        cssClass = 'ui-datepicker-current-day';
                    return [true, cssClass];
                },
                onChangeMonthYear: function (year, month, inst) {
                    showCalendar();
                    selectCurrentWeek();
                }
            });

            $('.week-picker .ui-datepicker-calendar tr').live('mousemove', function () { $(this).find('td a').addClass('ui-state-hover'); });
            $('.week-picker .ui-datepicker-calendar tr').live('mouseleave', function () { $(this).find('td a').removeClass('ui-state-hover'); });
        });
        function showCalendar() {
            $('#calendar').maxZIndex();
            $('#calendar').show();
        }
        function hideCalendar() {
            $('#calendar').hide();
        }
    </script>
</head>
<body>
    <input type="text" onfocus="javascript:showCalendar();" id="week" />
    <div class="week-picker" style="display: none; position: absolute;" id="calendar">
    </div>
    <br />
    <br />
    <label>
        Week :</label>
    <br />
    <label>
        Week :</label>
    <br />
    <label>
        Week :</label>
    <br />
    <label>
        Week :</label>
    <br />
    <label>
        Week :</label>
    <br />
    <label>
        Week :</label>
    <br />
    <label>
        Week :</label>
    <br />
    <label>
        Week :</label>
    <br />
    <label>
        Week :</label>
    <br />
    <label>
        Week :</label>
    <br />
    <label>
        Week :</label>
    <br />
    <label>
        Week :</label>
    <br />
    <label>
        Week :</label>
    <br />
    <label>
        Week :</label>
    <br />
    <label>
        Week :</label>
    <br />
    <label>
        Week :</label>
    <br />
    <label>
        Week :</label>
    <span id="startDate"></span>- <span id="endDate"></span>
</body>
</html>
