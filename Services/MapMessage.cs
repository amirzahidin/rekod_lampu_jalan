
using RekodLampuJalan.Models;

public record MoveMapMessage(double Latitude, double Longitude);
public record LocationPermissionDeniedMessage;
public record LocationErrorMessage(string ErrorMessage);
public record PopUpNewMapPinValue(MapPin mapPin);
public record PopUpNewMapPinClose;
public record DeleteFlyCableMessage(int id);
public record SummaryMoveMapMessage(double Latitude, double Longitude);
public record MapViewBusyMessage(bool IsBusy);
public record SummaryClearPinMessage();
public record MapsViewClearPinMessage();
