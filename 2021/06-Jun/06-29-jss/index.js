/**
 * Sample JSS code that doesnt use a frontend web framework.
 * @author Andrew Jarombek
 * @since 4/10/2021
 */

import jss from "jss";
import preset from "jss-preset-default";

jss.setup(preset());

const robotoSlabMixin = {
    fontStyle: 'normal',
    fontWeight: 'normal',
    fontFamily: 'RobotoSlab-Regular, Helvetica, serif'
}

const FeelColors = [
    '#EA9999',
    '#FFAD99',
    '#EAC199',
    '#FFD699',
    '#FFFFAD',
    '#E3E3E3',
    '#C7F599',
    '#99D699',
    '#99C199',
    '#A3A3FF'
];

const currentFeel = 6;

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
        minHeight: 500,
        backgroundColor: '#222',
        margin: 0,
        paddingTop: 25
    },
    exerciseLog: {
        width: '70%',
        backgroundColor: FeelColors[currentFeel],
        border: '2px solid rgb(136, 136, 136)',
        margin: '0 auto',
        padding: 7,
        boxShadow: '0 2px 4px 0 rgba(0, 0, 0, 0.2)',
        borderRadius: 3
    },
    headerSection: {
        display: 'flex'
    },
    titles: {
        display: 'block'
    },
    titleLink: {
        color: 'rgb(51, 51, 51)',
        fontSize: 16,
        textDecoration: 'none',
        ...robotoSlabMixin
    },
    title: {
        fontSize: 16,
        margin: 0,
        textDecoration: 'underline',
        ...robotoSlabMixin
    },
    metadata: {
        display: 'block',
        margin: '0 0 0 auto',

        '& > p': {
            margin: 0,
            fontSize: 14,
            textAlign: 'right',
            ...robotoSlabMixin
        }
    },
    bodySection: {
        marginTop: 20
    },
    dataField: {
        margin: 0,
        fontSize: 14,
        ...robotoSlabMixin
    },
    description: {
        marginTop: 10,
        fontSize: 14,
        ...robotoSlabMixin
    },
};

const { classes } = jss.createStyleSheet(styles).attach();

document.body.innerHTML = `
    <div class="${classes.page}">
        <div class="${classes.exerciseLog}">
            <div class="${classes.headerSection}">
                <div class="${classes.titles}">
                    <a class="${classes.titleLink}">Andrew Jarombek</a>
                    <h6 class="${classes.title}">Long Run</h6>
                </div>
                <div class="${classes.metadata}">
                    <p>Apr. 11th, 2021</p>
                    <p>RUN</p>
                </div>
            </div>
            <div class="${classes.bodySection}">
                <div>
                    <p class="${classes.dataField}">Location: Riverside, CT</p>
                    <p class="${classes.dataField}">14.01 miles</p>
                    <p class="${classes.dataField}">1:39:03 (7:04/mi)</p>
                </div>
                <div class="${classes.description}">
                    Went home to CT for the weekend to use the track on Saturday.  Got a nice long run in this morning 
                    as well, longest since last summer.
                </div>
            </div>
        </div>
    </div>
`;
