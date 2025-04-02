delete
from "AuthTokens"
where "ExpiresAtUtc" <= current_timestamp;

delete
from "UnregisteredUsers"
where "ExpiresAtUtc" <= current_timestamp;
