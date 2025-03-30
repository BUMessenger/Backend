delete
from "AuthToken"
where "ExpiresAt" <= current_timestamp;

delete
from "UnregistredUser"
where "ExpiresAt" <= current_timestamp;
