import { ResponseStatus } from "../common/enums/response-statuses";

export class ResponseObject{
    public Status: ResponseStatus;
    public StatusDescription: String;
    public EntityID: Number;
}