import http from 'k6/http';
import { sleep } from 'k6';
import { check } from 'k6';

export default function() {
    const response = http.get('https://test.k6.io');
    check(response, {
        'HTTP status is 200': (r) => r.status === 200,
        'QuickPizza exists': (r) => r.body.includes("QuickPizza")
    });
    sleep(1);
}