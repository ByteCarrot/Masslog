db.activities.ensureIndex({
    'StartedAt.Date': -1,
    'Status': 1,
    'FailureDeterminedBy': 1
});

db.activities.ensureIndex({
    'StartedAt.Date': -1,
    'ApplicationId': 1
});

db.activities.ensureIndex({
    'StartedAt.Date': -1,
    'ApplicationId': 1
});

db.activities.ensureIndex({
    'StartedAt.Date': -1,
    'StatusCode': 1
});