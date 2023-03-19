const schemaExample = {
    type: x => typeof x === 'string' && /[A-Za-z0-9]*/.test(x),  // can test by regex            
    prompt: x => typeof x === 'string' && /[A-Za-z0-9]*/.test(x), // can test by regex
    completion: x => typeof x === 'string' 
};

const validateObject = (obj, schema) => Object
    .keys(schema)
    .filter(key => !schema[key](obj[key]))
    .map(key => new Error(`${key}`));

// Apply object validation to every object in this array.  Returns count of errors.  Could easily extend to returning where the errors are.
// Pass in an array of objects plus a schema,
// Returns the number of validation errors found
const validateArrayOfObjects = (arr, schema) => arr
    .map(obj => validateObject(obj, schema))
    .reduce((acc, arr) => arr.length > 0 ? acc + 1 : 0, 0);  // use a fold to count up the number of entries whose length is > 0


/* Client side validation of data from USGS 
 * 
 * */

function validateMaterialData(data) {
    if (data === undefined || !Array.isArray(data))
        return false;
    const schema = {
        type: x => typeof x === 'string',
        prompt: x => typeof x === 'string',
        completion: x => typeof x === 'string',
    };

    return validateArrayOfObjects(data, schema) === 0;
}



export { validateObject, validateArrayOfObjects, validateMaterialData}