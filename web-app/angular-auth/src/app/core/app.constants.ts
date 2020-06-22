import { environment } from 'src/environments/environment';

export class AppConstants {

    public static API_RESOURCE = ((): string => {
        if (environment) {
            // dev
            return 'http://localhost:3200/api/';
        } else {


            // prod
            return 'http://localhost:xxx/api/';
        }
    })();
}
