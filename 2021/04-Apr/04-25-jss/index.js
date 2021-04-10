/**
 * Sample JSS code that doesnt use a frontend web framework.
 * @author Andrew Jarombek
 * @since 4/10/2021
 */

import jss from "jss";
import preset from "jss-preset-default";

jss.setup(preset());

const styles = {
    '@global': {
        body: {
            backgroundColor: '#000',
            margin: 0
        }
    },
    page: {
        width: '100%',
        height: '100vh',
        minHeight: '500px',
        backgroundColor: '#222',
        margin: 0
    }
};

const { classes } = jss.createStyleSheet(styles).attach();

document.body.innerHTML = `
    <div class="${classes.page}">
    
    </div>
`;
